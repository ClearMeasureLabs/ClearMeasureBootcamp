package ClearMeasureBootcamp

import ClearMeasureBootcamp.buildTypes.*
import ClearMeasureBootcamp.vcsRoots.*
import ClearMeasureBootcamp.vcsRoots.ClearMeasureBootcamp_HttpsGithubComClearMeasureLabsClearMeasureBootcamp
import jetbrains.buildServer.configs.kotlin.v10.*
import jetbrains.buildServer.configs.kotlin.v10.Project
import jetbrains.buildServer.configs.kotlin.v10.projectFeatures.VersionedSettings
import jetbrains.buildServer.configs.kotlin.v10.projectFeatures.VersionedSettings.*
import jetbrains.buildServer.configs.kotlin.v10.projectFeatures.versionedSettings

object Project : Project({
    uuid = "9e827182-f881-4c3d-b81b-1e5f3512ca53"
    extId = "ClearMeasureBootcamp"
    parentId = "_Root"
    name = "ClearMeasureBootcamp"
    description = "Clear Measure Bootcamp Repo"

    vcsRoot(ClearMeasureBootcamp_HttpsGithubComClearMeasureLabsClearMeasureBootcamp)

    buildType(ClearMeasureBootcamp_1IntegrationBuild)
    buildType(ClearMeasureBootcamp_3)
    buildType(ClearMeasureBootcamp_2DeployToTest)
    buildType(ClearMeasureBootcamp_4)

    params {
        password("OctopusDeployAPIKey", "zxxdd7f7a1df9a2a005efc0d25ef72b4e57ece057505fd1211fd7b3bfb7822ce1e3", display = ParameterDisplay.HIDDEN)
    }

    features {
        versionedSettings {
            id = "PROJECT_EXT_2"
            mode = VersionedSettings.Mode.ENABLED
            buildSettingsMode = VersionedSettings.BuildSettingsMode.PREFER_SETTINGS_FROM_VCS
            rootExtId = ClearMeasureBootcamp_HttpsGithubComClearMeasureLabsClearMeasureBootcamp.extId
            showChanges = false
            settingsFormat = VersionedSettings.Format.KOTLIN
        }
    }

    cleanup {
        history(builds = 10)
        artifacts(builds = 3)
        preventDependencyCleanup = true
    }
})
