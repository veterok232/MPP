using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGeneratorLib.Model;
using TestGeneratorLib.Model.Class;

namespace TestGeneratorLib.Block
{
    /// <summary>
    ///     Code analyzer class gets all needed information about source code
    /// </summary>
    public class CodeAnalyzer
    {
        /// <summary>
        ///     Gets all information in file
        /// </summary>
        /// 
        /// <param name="code">Source code</param>
        /// <returns>FileData</returns>
        public static FileData GetFileData(string code)
        {
            CompilationUnitSyntax codeRoot = CSharpSyntaxTree.ParseText(code).GetCompilationUnitRoot();
            var classesData = new List<ClassData>();
            foreach (ClassDeclarationSyntax classData in codeRoot.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                classesData.Add(GetClassData(classData));
            }
            return new FileData(classesData);
        }

        /// <summary>
        ///     Get all information about class
        /// </summary>
        /// 
        /// <param name="classNode">Class node information</param>
        /// <returns>ClassData</returns>
        private static ClassData GetClassData(ClassDeclarationSyntax classNode)
        {
            var constructorsData = GetConstructorsInfo(classNode);
            var methodsData = GetMethodsInfo(classNode);
            return new ClassData(classNode.Identifier.ValueText, constructorsData, methodsData);
        }

        /// <summary>
        /// Gets information about constructors
        /// </summary>
        /// 
        /// <param name="classNode">Class node information</param>
        /// <returns>List<ConstructorData></returns>
        private static List<ConstructorData> GetConstructorsInfo(ClassDeclarationSyntax classNode)
        {
            var allConstructors = classNode.DescendantNodes().OfType<ConstructorDeclarationSyntax>()
                .Where((constructorData) => constructorData.Modifiers.Any((modifier) => modifier.IsKind(SyntaxKind.PublicKeyword)));
            var constructorsData = new List<ConstructorData>();
            foreach (var constructorData in allConstructors)
            {
                constructorsData.Add(GetConstructorData(constructorData));
            }
            return constructorsData;
        }

        /// <summary>
        /// Gets information about methods
        /// </summary>
        /// 
        /// <param name="classNode">Class node information</param>
        /// <returns>List<MethodData></returns>
        private static List<MethodData> GetMethodsInfo(ClassDeclarationSyntax classNode)
        {
            var allMethods = classNode.DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Where((methodData) => methodData.Modifiers.Any((modifier) => modifier.IsKind(SyntaxKind.PublicKeyword));
            var methodsData = new List<MethodData>();
            foreach (var methodData in allMethods)
            {
                methodsData.Add(GetMethodData(methodData));
            }
            return methodsData;
        }

        /// <summary>
        ///     Get all constructors data
        /// </summary>
        /// 
        /// <param name="constructorNode">Constructor node information</param>
        /// <returns>ConstructorData</returns>
        private static ConstructorData GetConstructorData(ConstructorDeclarationSyntax constructorNode)
        {
            var parameters = new Dictionary<string, string>();
            foreach (var parameter in constructorNode.ParameterList.Parameters)
            {
                parameters.Add(parameter.Identifier.Text, parameter.Type.ToString());
            }
            return new ConstructorData(constructorNode.Identifier.ValueText, parameters);
        }

        /// <summary>
        /// Get all methods data
        /// </summary>
        /// 
        /// <param name="methodNode">Method node information</param>
        /// <returns>MethodData</returns>
        private static MethodData GetMethodData(MethodDeclarationSyntax methodNode)
        {
            var parameters = new Dictionary<string, string>();
            foreach (var parameter in methodNode.ParameterList.Parameters)
            {
                parameters.Add(parameter.Identifier.Text, parameter.Type.ToString());
            }
            return new MethodData(methodNode.Identifier.ValueText, parameters, methodNode.ReturnType.ToString());
        }   
    }
}
