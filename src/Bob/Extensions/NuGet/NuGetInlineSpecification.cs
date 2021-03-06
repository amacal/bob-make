﻿using Bob.Core;
using System;
using System.Text;
using System.Xml.Linq;

namespace Bob.Extensions.NuGet
{
    public class NuGetInlineSpecification : NuGetSpecification
    {
        private readonly Action<NuGetInlineParameters> parameters;

        public NuGetInlineSpecification(Action<NuGetInlineParameters> parameters)
        {
            this.parameters = parameters;
        }

        public string Resolve()
        {
            NuGetInlineParameters instance = new NuGetInlineParameters
            {
                Dependencies = new NuGetInlineDependenciesCollection(),
                Files = new NuGetInlineFilesCollection()
            };

            this.parameters(instance);
            return this.Resolve(instance);
        }

        private string Resolve(NuGetInlineParameters data)
        {
            XNamespace xmlns = "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd";
            string path = Container.Storage.Temp.New(".nuspec");

            XElement files = new XElement(xmlns + "files");
            XElement dependencies = new XElement(xmlns + "dependencies");

            foreach (string target in data.Files.Targets)
            {
                foreach (string file in data.Files[target].Execute())
                {
                    files.Add(
                        new XElement(xmlns + "file",
                            new XAttribute("src", file.Backslash()),
                            new XAttribute("target", target)));
                }
            }

            foreach (NuGetPackage package in data.Dependencies.Packages)
            {
                dependencies.Add(
                    new XElement(xmlns + "dependency",
                        new XAttribute("id", package.Id),
                        new XAttribute("version", package.Version)));
            }

            XDocument document =
                new XDocument(
                    new XDeclaration("1.0", Encoding.UTF8.BodyName, "yes"),
                    new XElement(xmlns + "package",
                        new XElement(xmlns + "metadata",
                            new XElement(xmlns + "id", data.Id),
                            new XElement(xmlns + "version", data.Version),
                            new XElement(xmlns + "authors", data.Authors),
                            new XElement(xmlns + "description", data.Description),
                            dependencies),
                        files));

            Container.Storage.WriteText(path, document.ToString());
            return path;
        }
    }
}