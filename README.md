# LogiSimLoader
## _Parse LogiSim files into C# objects_

<img alt="DotNet" src="https://www.vectorlogo.zone/logos/dotnet/dotnet-icon.svg" width="40" height="40" style="display: inline;" />
<img alt="NuGet" src="https://www.vectorlogo.zone/logos/nuget/nuget-icon.svg" width="40" height="40" style="display: inline;" />
<img alt="XML" src="https://www.vectorlogo.zone/logos/w3c_xml/w3c_xml-icon.svg" width="40" height="40" style="display: inline;" />

[![Get LogiSim now](https://upload.wikimedia.org/wikipedia/commons/b/ba/Logisim-icon.svg)](https://sourceforge.net/projects/circuit/)

[NuGet package](https://www.nuget.org/packages/LogiSimLoader)

LogiSimLoader is a C# (.NET Standard 2.1) library implementing XML parsing to allow for import, modification and export of LogiSim files.

## Features

- Supports multi-circuit files
- Preserves appearance of circuits, attributes of components, etc.
- Supports all LogiSim libraries (limited support)

## Technologies

Technologies used to make LogiSimLoader possible:
- NuGet - for package distribution -> [NuGet package](https://www.nuget.org/packages/LogiSimLoader)
- System.Xml - for XML management
- LogiSim - the circuit simulation tool itself

## Usage
`using LogiSimLoader;`

then,
```csharp
LSFile file = LS.LoadFromFile("circuit.circ");
file.circuits.<...>
file.circuits[0].components.<...>
file.libraries.<...>
LS.ExportToFile(file, "circuit_2.circ");
```

## Development
No contributions.

## License
MIT