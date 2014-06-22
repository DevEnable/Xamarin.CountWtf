namespace CounterWtf.PCL

open System.Collections.Generic
open System.Threading.Tasks
open Microsoft.WindowsAzure.MobileServices

    type IProjectStore = 
        abstract member GetProjects : unit -> Task<IEnumerable<Project>>


    module MobileClientFactory =
        [<Literal>]
        let private applicationURL = @"https://counterwtf.azure-mobile.net/";
        [<Literal>]
        let private applicationKey = @"ddjldEDUWmSdHnMurGuEbAOJDtCEOA59";

        let getClient() =
            new MobileServiceClient(applicationURL, applicationKey)

    
    type MobileServicesProjectStore() =
        let client = MobileClientFactory.getClient()

        interface IProjectStore with
            member x.GetProjects() = client.GetTable<Project>().ToEnumerableAsync()