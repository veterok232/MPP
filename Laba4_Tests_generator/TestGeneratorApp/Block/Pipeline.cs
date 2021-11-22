using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TestGeneratorLib.Block;

namespace TestGeneratorMain.Block
{
    /// <summary>
    ///     Pipelines
    /// </summary>
    public class Pipeline
    {
        /// <summary>
        ///     Generate tests
        /// </summary>
        /// 
        /// <param name="sourcePath">Src</param>
        /// <param name="fileNames">Files</param>
        /// <param name="destinationPath">Dest</param>
        /// <param name="maxPipelineTasks">Max pipelines count</param>
        /// <returns>Task</returns>
        public Task Generate(string sourcePath, string[] fileNames, string destinationPath, int maxPipelineTasks)
        {
            Directory.CreateDirectory(destinationPath);

            var execOptions = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = maxPipelineTasks };
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            // Read files
            var downloadStringBlock = new TransformBlock<string, string>
            (
                async path =>
                {
                    using (var reader = new StreamReader(path))
                    {
                        return await reader.ReadToEndAsync();
                    }
                },
                execOptions
            );

            // Generate tests
            var generateTestsBlock = new TransformManyBlock<string, KeyValuePair<string, string>>
            (
                async sourceCode =>
                {
                    var fileInfo = await Task.Run(() => CodeAnalyzer.GetFileData(sourceCode));
                    return await Task.Run(() => TestGenerator.GenerateTests(fileInfo));
                },
                execOptions
            );

            // Write tests to files
            var writeFileBlock = new ActionBlock<KeyValuePair<string, string>>
            (
                async fileNameCodePair =>
                {
                    using (var writer = new StreamWriter(destinationPath + '\\' + fileNameCodePair.Key + ".cs"))
                    {
                        await writer.WriteAsync(fileNameCodePair.Value);
                    }
                },
                execOptions
            );

            // Complete tasks (union blocks)
            downloadStringBlock.LinkTo(generateTestsBlock, linkOptions);
            generateTestsBlock.LinkTo(writeFileBlock, linkOptions);
            foreach (var fileName in fileNames)
            {
                downloadStringBlock.Post(sourcePath + @"\" + fileName);
            }

            downloadStringBlock.Complete();
            return writeFileBlock.Completion;
        }
    }
}
