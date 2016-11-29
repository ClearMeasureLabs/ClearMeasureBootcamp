package ClearMeasureBootcamp.buildTypes

import ClearMeasureBootcamp.buildFeatures.githubStatus
import ClearMeasureBootcamp.buildFeatures.xmlReport
import ClearMeasureBootcamp.buildParams.octoParams
import ClearMeasureBootcamp.buildSteps.ci
import jetbrains.buildServer.configs.kotlin.v10.*
import jetbrains.buildServer.configs.kotlin.v10.BuildFeature
import jetbrains.buildServer.configs.kotlin.v10.BuildFeature.*
import jetbrains.buildServer.configs.kotlin.v10.buildFeatures.FreeDiskSpace
import jetbrains.buildServer.configs.kotlin.v10.buildFeatures.FreeDiskSpace.*
import jetbrains.buildServer.configs.kotlin.v10.buildFeatures.VcsLabeling
import jetbrains.buildServer.configs.kotlin.v10.buildFeatures.VcsLabeling.*
import jetbrains.buildServer.configs.kotlin.v10.buildFeatures.freeDiskSpace
import jetbrains.buildServer.configs.kotlin.v10.buildFeatures.vcsLabeling
import jetbrains.buildServer.configs.kotlin.v10.buildSteps.ExecBuildStep
import jetbrains.buildServer.configs.kotlin.v10.buildSteps.ExecBuildStep.*
import jetbrains.buildServer.configs.kotlin.v10.buildSteps.exec
import jetbrains.buildServer.configs.kotlin.v10.triggers.ScheduleTrigger
import jetbrains.buildServer.configs.kotlin.v10.triggers.ScheduleTrigger.*
import jetbrains.buildServer.configs.kotlin.v10.triggers.VcsTrigger
import jetbrains.buildServer.configs.kotlin.v10.triggers.VcsTrigger.*
import jetbrains.buildServer.configs.kotlin.v10.triggers.schedule
import jetbrains.buildServer.configs.kotlin.v10.triggers.vcs

object ClearMeasureBootcamp_1IntegrationBuild : BuildType({
    uuid = "dc497fe7-0941-4d10-a04b-644aee7a3192"
    extId = "ClearMeasureBootcamp_1IntegrationBuild"
    name = "1 - Integration Build"
    description = "Commit stage build process including unit tests and component-level integration tests"

    artifactRules = """build\*.%build.number%.nupkg
build\test\* => test
src\packages\NUnit.Runners*\** => nunit
src\packages\NUnit.Console*\** => nunit3
src\packages\SpecFlow*\tools\* => specflow"""
    buildNumberPattern = "2.0.%build.counter%"

    params {
        octoParams()
    }

    vcs {
        root(ClearMeasureBootcamp.vcsRoots.ClearMeasureBootcamp_HttpsGithubComClearMeasureLabsClearMeasureBootcamp)

    }

    steps {
        ci()
    }

    triggers {
        vcs {
            quietPeriodMode = VcsTrigger.QuietPeriodMode.USE_CUSTOM
            quietPeriod = 15
        }
        schedule {
            schedulingPolicy = daily {
                hour = 6
            }
            triggerBuild = always()
            withPendingChangesOnly = false
            param("revisionRule", "lastFinished")
            param("dayOfWeek", "Sunday")
        }
    }

    features {
        xmlReport()
        githubStatus()
        vcsLabeling {
            vcsRootExtId = "__ALL__"
            successfulOnly = true
            branchFilter = "+:*"
        }
        freeDiskSpace {
            requiredSpace = "5gb"
            failBuild = false
        }
        feature {
            type = "perfmon"
        }
    }

    requirements {
        exists("env.=C:")
    }
})
