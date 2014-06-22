namespace CounterWtf.PCL

open System
open Newtonsoft.Json

// Could use base data in EntityData, but don't really care about it for the purposes of this demo.
[<CLIMutable>]
type Project = 
    { 
        [<JsonProperty("name")>]
        Name : String
        [<JsonProperty("createdBy")>]
        CreatedBy : String
    }
    
[<CLIMutable>]
type WTF =
    {
        [<JsonProperty("projectId")>]
        ProjectId : String
        [<JsonProperty("createdBy")>]
        CreatedBy : String
    }