package ClearMeasureBootcamp.buildTypes

import ClearMeasureBootcamp.buildSteps.octopusPromoteRelease
import ClearMeasureBootcamp.buildTypes.ClearMeasureBootcamp_3
import jetbrains.buildServer.configs.kotlin.v10.*
import jetbrains.buildServer.configs.kotlin.v10.BuildStep
import jetbrains.buildServer.configs.kotlin.v10.BuildStep.*
import jetbrains.buildServer.configs.kotlin.v10.triggers.FinishBuildTrigger
import jetbrains.buildServer.configs.kotlin.v10.triggers.FinishBuildTrigger.*
import jetbrains.buildServer.configs.kotlin.v10.triggers.finishBuildTrigger

object ClearMeasureBootcamp_4 : BuildType({
    uuid = "2089e5c1-45e9-43c5-93f8-75d0de57be56"
    extId = "ClearMeasureBootcamp_4"
    name = "4 - Promote to Production"

    buildNumberPattern = "%dep.ClearMeasureBootcamp_1IntegrationBuild.build.number%"

    var environmentFrom = "Clear Measure Bootcamp Staging"
    var environmentTo = "Clear Measure Bootcamp Production"

    steps {
        octopusPromoteRelease(environmentFrom, environmentTo)
    }

    triggers {
        finishBuildTrigger {
            buildTypeExtId = ClearMeasureBootcamp_3.extId
            successfulOnly = true
        }
    }

    dependencies {
        dependency(ClearMeasureBootcamp.buildTypes.ClearMeasureBootcamp_3) {
            snapshot {
            }
        }
        artifacts(ClearMeasureBootcamp.buildTypes.ClearMeasureBootcamp_1IntegrationBuild) {
            cleanDestination = true
            artifactRules = """*
**"""
        }
    }

    requirements {
        exists("env.=C:")
    }
})
