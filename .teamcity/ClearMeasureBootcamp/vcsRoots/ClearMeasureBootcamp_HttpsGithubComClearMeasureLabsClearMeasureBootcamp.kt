package ClearMeasureBootcamp.vcsRoots

import jetbrains.buildServer.configs.kotlin.v10.*
import jetbrains.buildServer.configs.kotlin.v10.vcs.GitVcsRoot

object ClearMeasureBootcamp_HttpsGithubComClearMeasureLabsClearMeasureBootcamp : GitVcsRoot({
    uuid = "bb074878-6c36-4a10-98e7-3aa11564f532"
    extId = "ClearMeasureBootcamp_HttpsGithubComClearMeasureLabsClearMeasureBootcamp"
    name = "https://github.com/ClearMeasureLabs/ClearMeasureBootcamp"
    url = "git@github.com:ClearMeasureLabs/ClearMeasureBootcamp.git"
    branch = "master"
    branchSpec = "+:refs/heads/*"
    authMethod = uploadedKey {
        uploadedKey = "id_rsa"
    }
})
