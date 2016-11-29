package ClearMeasureBootcamp.buildParams

/**
 * Created by Yujie on 11/7/2016.
 */
import jetbrains.buildServer.configs.kotlin.v10.BuildTypeSettings

fun BuildTypeSettings.octoParams() {
    params {
        param("env.Configuration", "Release")
        param("env.RunOctoPack", "true")
        param("env.Version", "%build.number%")
    }
}