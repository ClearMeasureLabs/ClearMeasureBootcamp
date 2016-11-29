package ClearMeasureBootcamp.buildTypes

import ClearMeasureBootcamp.buildSteps.octopusPromoteRelease
import ClearMeasureBootcamp.buildTypes.ClearMeasureBootcamp_2DeployToTest
import jetbrains.buildServer.configs.kotlin.v10.*
import jetbrains.buildServer.configs.kotlin.v10.BuildStep
import jetbrains.buildServer.configs.kotlin.v10.BuildStep.*
import jetbrains.buildServer.configs.kotlin.v10.triggers.FinishBuildTrigger
import jetbrains.buildServer.configs.kotlin.v10.triggers.FinishBuildTrigger.*
import jetbrains.buildServer.configs.kotlin.v10.triggers.finishBuildTrigger

object ClearMeasureBootcamp_3 : BuildType({
    uuid = "d199f7e6-f2d7-4a46-9a65-385a1bedd6e4"
    extId = "ClearMeasureBootcamp_3"
    name = "3 - Promote to Staging"

    buildNumberPattern = "%dep.ClearMeasureBootcamp_1IntegrationBuild.build.number%"

    var environmentFrom = "Clear Measure Bootcamp Test"
    var environmentTo = "Clear Measure Bootcamp Staging"

    steps {
        octopusPromoteRelease(environmentFrom, environmentTo)
    }

    triggers {
        finishBuildTrigger {
            buildTypeExtId = ClearMeasureBootcamp_2DeployToTest.extId
            successfulOnly = true
            branchFilter = """+:<default>
+:develop"""
        }
    }

    dependencies {
        dependency(ClearMeasureBootcamp.buildTypes.ClearMeasureBootcamp_2DeployToTest) {
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
