#r @"packages/FAKE/tools/FakeLib.dll"
open Fake

let srcBuildDir = "./build-src/"

Target "clean" (fun _ ->
    CleanDir srcBuildDir
)

Target "compile-src" (fun _ -> 
    !! "./LogAnalyzer/*.csproj"
        |> MSBuildRelease srcBuildDir "Build"
        |> Log "compile-src output: "
)

Target "complete" (fun _ ->
    log "finished build"
)

"clean"
==> "compile-src"
==> "complete"

RunTargetOrDefault "complete"