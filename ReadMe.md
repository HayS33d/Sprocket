
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

- [Jason Riley] Rileyja@protonmail.com


## Features

- Add Parameters with a single call
- Convert Json or XML to Object<T>
- Disposable


## Support

For support, email rileyja@protonmail.com


## 🚀 About Me
I am not your normal application developer. I can weld, rebuild engines, 
wire houses (electrical), plumb houses, hunt, fish and brew beer 🍺



## XML -> Json Deserialization GenericListConverter<T>
When older sql servers such as SQL 2016 have stored proceedures that only export data AS XML, if an item that is expected to be a collection
only has 1 object Json Deserialization will throw and error expecting a collection.  
# Usage
     public class IttyBittyExample
        {
            [JsonConverter(typeof(GenericListConverter<string>))]
            public List<string> TinyThingie { get; set; }
        }

This will ensure if XML being converterted to Json will deserialize a single 


    <IttyBittyExample>
        <TinyThingie>litte thing</TinyThingie>
    </IttyBittyExample>

This will deserialize TinyThingie to List<string> instead of a single string object.


