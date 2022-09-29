
# SprocKit

SprocKit (Sprocket) is a library you can use to call Strored Procedures within a SqlServer and 
convert the response Json or XML data to a Deserialized Object <T>.



## Usage Example

    using SprocketClient sqlClient = new SprocketClient(new List<ISqlFilter>(){new CustomAnimalFilter()}), Configutation["connectionString"]);
    ISqlRequest request = sqlClient.Request("[dbo].[spGetSomeDataInJson]");
    request.AddParameter("@myCustomId", 123);
    request.AddParameter("@animalType", "dog");
    request.AddParameter("@processed", true);
    AnimalList al = request.As<AnimalList>();


## Authors

- [Jason Riley](mailto:rileyja@protonmail.com)


## Features

- Add Parameters with a single call
- Convert Json or XML to Object<T>
- Disposable

## Support

For support, email rileyja@protonmail.com(mailto:rileyja@protonmail.com)


## 🚀 About Me
I am not your normal application developer. I can weld, rebuild engines, 
wire houses (electrical), plumb houses, hunt, fish and brew beer 🍺

 