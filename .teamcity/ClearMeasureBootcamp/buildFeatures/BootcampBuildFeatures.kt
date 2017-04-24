package ClearMeasureBootcamp.buildFeatures

/**
 * Created by Yujie on 11/7/2016.
 */
import jetbrains.buildServer.configs.kotlin.v10.BuildFeatures

fun BuildFeatures.githubStatus() {
    feature {
        type = "teamcity.github.status"
        param("github_report_on", "on start and finish")
        param("guthub_authentication_type", "password")
        param("guthub_comments", "true")
        param("guthub_context", "continuous-integration/teamcity")
        param("guthub_guest", "true")
        param("guthub_host", "https://api.github.com")
        param("guthub_owner", "ClearMeasureLabs")
        param("guthub_repo", "ClearMeasureBootcamp")
        param("guthub_username", "justinmason")
        param("secure:guthub_username", "zxx57dba62fe4fcf32b775d03cbe80d301b")
    }
}

fun BuildFeatures.xmlReport() {
    feature {
        type = "xml-report-plugin"
        param("xmlReportParsing.reportDirs", "build/*Result.xml")
        param("xmlReportParsing.reportType", "nunit")
    }
}