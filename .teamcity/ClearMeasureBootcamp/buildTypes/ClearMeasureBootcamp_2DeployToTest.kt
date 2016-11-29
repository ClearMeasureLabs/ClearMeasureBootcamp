package ClearMeasureBootcamp.buildTypes

import ClearMeasureBootcamp.buildSteps.octopusCreateRelease
import ClearMeasureBootcamp.buildSteps.smokeTest
import jetbrains.buildServer.configs.kotlin.v10.BuildType
import jetbrains.buildServer.configs.kotlin.v10.FailureAction
import jetbrains.buildServer.configs.kotlin.v10.failureConditions.BuildFailureOnMetric
import jetbrains.buildServer.configs.kotlin.v10.failureConditions.failOnMetricChange
import jetbrains.buildServer.configs.kotlin.v10.triggers.finishBuildTrigger

object ClearMeasureBootcamp_2DeployToTest : BuildType({
    uuid = "2404cd24-93b6-48c3-b53a-8fb0a6165e8f"
    extId = "ClearMeasureBootcamp_2DeployToTest"
    name = "2 - Deploy to Test"

    artifactRules = """%teamcity.agent.work.dir%\**\testresults\* => errorscreenshots
"C:\acceptancetests-Clear Measure Bootcamp Test\testresults\"* => testerrors
%system.teamcity.build.workingDir%\**\testresults\* => testerrorscreens
%system.teamcity.build.workingDir%\AcceptanceTestResult.*"""
    buildNumberPattern = "%dep.ClearMeasureBootcamp_1IntegrationBuild.build.number%"

    var environment = "Clear Measure Bootcamp Test"
    steps {
        octopusCreateRelease(environment)
//        smokeTest()
    }

    triggers {
        finishBuildTrigger {
            buildTypeExtId = ClearMeasureBootcamp_1IntegrationBuild.extId
            successfulOnly = true
            branchFilter = "+:*"
        }
    }

    failureConditions {
        failOnMetricChange {
            metric = BuildFailureOnMetric.MetricType.TEST_COUNT
            threshold = 20
            units = BuildFailureOnMetric.MetricUnit.PERCENTS
            comparison = BuildFailureOnMetric.MetricComparison.LESS
            compareTo = build {
                buildRule = lastSuccessful()
            }
        }
    }

    dependencies {
        dependency(ClearMeasureBootcamp.buildTypes.ClearMeasureBootcamp_1IntegrationBuild) {
            snapshot {
                onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = """*
**"""
            }
        }
    }

    requirements {
        exists("env.=C:")
    }
})


