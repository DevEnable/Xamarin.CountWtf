namespace CounterWtf.PCL

open System
open System.Collections.Generic
open Newtonsoft.Json

// Could use base data in EntityData, but don't really care about it for the purposes of this demo.
[<CLIMutable>]
type Project = 
    { 
        [<JsonProperty("id")>]
        Id : string
        [<JsonProperty("name")>]
        Name : String
        [<JsonProperty("createdBy")>]
        CreatedBy : String
    }

[<CLIMutable>]
type ProjectSummary = 
    { 
        [<JsonProperty("id")>]
        Id : string
        [<JsonProperty("name")>]
        Name : String
        [<JsonProperty("createdBy")>]
        CreatedBy : String
        [<JsonProperty("wtfCount")>]
        WtfCount : int
    }

[<CLIMutable>]
type Wtf =
    {
        [<JsonProperty("projectId")>]
        ProjectId : String
        [<JsonProperty("createdBy")>]
        CreatedBy : String
    }