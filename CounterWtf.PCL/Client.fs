namespace CounterWtf.PCL

open System.Collections.Generic
open System.Threading.Tasks
open Microsoft.WindowsAzure.MobileServices

    type IProjectStore = 
        abstract member AddProject : Project -> Task
        abstract member GetProjectSummaries : unit -> Task<IEnumerable<ProjectSummary>>
        abstract member AddWtf : Wtf -> Task
        abstract member GetWtfs : string -> Task<IEnumerable<Wtf>>

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
            member x.AddProject project =
                client.GetTable<Project>()
                      .InsertAsync(project)
            member x.GetProjectSummaries() = 
                client.GetTable<ProjectSummary>()
                      .ToEnumerableAsync()
            member x.AddWtf wtf =
                client.GetTable<Wtf>()
                      .InsertAsync(wtf)
            member x.GetWtfs projectId = 
                client.GetTable<Wtf>()
                      .Where(fun wtf -> wtf.ProjectId = projectId)
                      .ToEnumerableAsync()