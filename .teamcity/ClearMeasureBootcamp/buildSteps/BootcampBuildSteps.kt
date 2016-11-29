package ClearMeasureBootcamp.buildSteps

/**
 * Created by Yujie on 11/7/2016.
 */

import jetbrains.buildServer.configs.kotlin.v10.BuildStep
import jetbrains.buildServer.configs.kotlin.v10.BuildSteps
import jetbrains.buildServer.configs.kotlin.v10.buildSteps.exec
import jetbrains.buildServer.configs.kotlin.v10.buildSteps.script

fun BuildSteps.ci() {
        exec {
            path = "ci.bat"
        }
}

fun BuildSteps.octopusCreateRelease(environment: String) {
    step {
        type = "octopus.create.release"
        executionMode = BuildStep.ExecutionMode.DEFAULT
        param("octopus_deployto", environment)
        param("octopus_host", "http://build.clear-measure.com:81/")
        param("octopus_project_name", "Deployment Pipeline")
        param("octopus_releasenumber", "%build.number%")
        param("octopus_version", "3.0+")
        param("octopus_waitfordeployments", "true")
        param("secure:octopus_apikey", "zxxe29f59f7fd27d0d79c6455d3e9795d3205c30b133f122abb")
    }
}

fun BuildSteps.octopusPromoteRelease(environmentFrom: String, environmentTo: String) {
    step {
        type = "octopus.promote.release"
        executionMode = BuildStep.ExecutionMode.DEFAULT
        param("octopus_deployto", environmentTo)
        param("octopus_host", "http://build.clear-measure.com:81/")
        param("octopus_project_name", "Deployment Pipeline")
        param("octopus_promotefrom", environmentFrom)
        param("octopus_version", "3.0+")
        param("octopus_waitfordeployments", "true")
        param("secure:octopus_apikey", "zxxe29f59f7fd27d0d79c6455d3e9795d3205c30b133f122abb")
    }
}

fun BuildSteps.octopusPromoteReleaseNoWait(environmentFrom: String, environmentTo: String) {
    step {
        type = "octopus.promote.release"
        executionMode = BuildStep.ExecutionMode.DEFAULT
        param("octopus_deployto", environmentTo)
        param("octopus_host", "http://build.clear-measure.com:81/")
        param("octopus_project_name", "Deployment Pipeline")
        param("octopus_promotefrom", environmentFrom)
        param("octopus_version", "3.0+")
        param("octopus_waitfordeployments", "false")
        param("secure:octopus_apikey", "zxxe29f59f7fd27d0d79c6455d3e9795d3205c30b133f122abb")
    }
}

fun BuildSteps.smokeTest() {
    script {
        executionMode = BuildStep.ExecutionMode.RUN_ON_FAILURE
        workingDir = """C:\acceptancetests-Clear Measure Bootcamp Test"""
        scriptContent = """.\NUnit.Console.3.0.1\tools\nunit3-console.exe .\ClearMeasure.Bootcamp.SmokeTests.dll"""
    }
}
