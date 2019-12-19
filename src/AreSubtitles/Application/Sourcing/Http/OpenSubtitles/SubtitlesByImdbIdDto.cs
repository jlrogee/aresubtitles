using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.Sourcing.Http.OpenSubtitles
{
    public class SubtitlesByImdbIdDtoSimple
    {
        [JsonProperty("SubDownloadLink")]
        public string SubDownloadLink { get; set; }

        [JsonProperty("ZipDownloadLink")]
        public string ZipDownloadLink { get; set; }
        
        [JsonProperty("SubFormat")]
        public string SubFormat { get; set; }
        
        [JsonProperty("Score")]
        public double Score { get; set; }

        [JsonProperty("Iso639")]
        public LanguageIso639 Language { get; set; }
    }
    
    public class SubtitlesByImdbIdDto
    {
        [JsonProperty("MatchedBy")]
        public string MatchedBy { get; set; }

        [JsonProperty("IDSubMovieFile")]
        public long IdSubMovieFile { get; set; }

        [JsonProperty("MovieHash")]
        public long MovieHash { get; set; }

        [JsonProperty("MovieByteSize")]
        public long MovieByteSize { get; set; }

        [JsonProperty("MovieTimeMS")]
        public long MovieTimeMs { get; set; }

        [JsonProperty("IDSubtitleFile")]
        public long IdSubtitleFile { get; set; }

        [JsonProperty("SubFileName")]
        public string SubFileName { get; set; }

        [JsonProperty("SubActualCD")]
        public long SubActualCd { get; set; }

        [JsonProperty("SubSize")]
        public long SubSize { get; set; }

        [JsonProperty("SubHash")]
        public string SubHash { get; set; }

        [JsonProperty("SubTSGroup")]
        public long SubTsGroup { get; set; }

        [JsonProperty("InfoReleaseGroup")]
        public string InfoReleaseGroup { get; set; }

        [JsonProperty("InfoFormat")]
        public string InfoFormat { get; set; }

        [JsonProperty("InfoOther")]
        public string InfoOther { get; set; }

        [JsonProperty("IDSubtitle")]
        public long IdSubtitle { get; set; }

        [JsonProperty("UserID")]
        public long UserId { get; set; }

        [JsonProperty("SubLanguageID")]
        public string SubLanguageId { get; set; }

        [JsonProperty("SubFormat")]
        public string SubFormat { get; set; }

        [JsonProperty("SubSumCD")]
        public long SubSumCd { get; set; }

        [JsonProperty("SubAuthorComment")]
        public string SubAuthorComment { get; set; }

        [JsonProperty("SubBad")]
        public long SubBad { get; set; }

        [JsonProperty("SubRating")]
        public string SubRating { get; set; }

        [JsonProperty("SubSumVotes")]
        public long SubSumVotes { get; set; }

        [JsonProperty("SubDownloadsCnt")]
        public long SubDownloadsCnt { get; set; }

        [JsonProperty("MovieReleaseName")]
        public string MovieReleaseName { get; set; }

        [JsonProperty("MovieFPS")]
        public string MovieFps { get; set; }

        [JsonProperty("IDMovie")]
        public long IdMovie { get; set; }

        [JsonProperty("IDMovieImdb")]
        public long IdMovieImdb { get; set; }

        [JsonProperty("MovieName")]
        public string MovieName { get; set; }

        [JsonProperty("MovieNameEng")]
        public string MovieNameEng { get; set; }

        [JsonProperty("MovieYear")]
        public long MovieYear { get; set; }

        [JsonProperty("MovieImdbRating")]
        public string MovieImdbRating { get; set; }

        [JsonProperty("SubFeatured")]
        public long SubFeatured { get; set; }

        [JsonProperty("UserNickName")]
        public string UserNickName { get; set; }

        [JsonProperty("SubTranslator")]
        public string SubTranslator { get; set; }


        [JsonProperty("LanguageName")]
        public string LanguageName { get; set; }

        [JsonProperty("SubComments")]
        public long SubComments { get; set; }

        [JsonProperty("SubHearingImpaired")]
        public long SubHearingImpaired { get; set; }

        [JsonProperty("UserRank")]
        public string UserRank { get; set; }

        [JsonProperty("SeriesSeason")]
        public long SeriesSeason { get; set; }

        [JsonProperty("SeriesEpisode")]
        public long SeriesEpisode { get; set; }

        [JsonProperty("MovieKind")]
        public string MovieKind { get; set; }

        [JsonProperty("SubHD")]
        public long SubHd { get; set; }

        [JsonProperty("SeriesIMDBParent")]
        public long SeriesImdbParent { get; set; }

        [JsonProperty("SubEncoding")]
        public string SubEncoding { get; set; }

        [JsonProperty("SubAutoTranslation")]
        public long SubAutoTranslation { get; set; }

        [JsonProperty("SubForeignPartsOnly")]
        public long SubForeignPartsOnly { get; set; }

        [JsonProperty("SubFromTrusted")]
        public long SubFromTrusted { get; set; }

        [JsonProperty("QueryCached")]
        public long QueryCached { get; set; }

        [JsonProperty("SubTSGroupHash")]
        public string SubTsGroupHash { get; set; }

        [JsonProperty("SubDownloadLink")]
        public string SubDownloadLink { get; set; }

        [JsonProperty("ZipDownloadLink")]
        public string ZipDownloadLink { get; set; }

        [JsonProperty("SubtitlesLink")]
        public string SubtitlesLink { get; set; }

        [JsonProperty("QueryNumber")]
        public long QueryNumber { get; set; }

//        [JsonProperty("QueryParameters")]
//        public QueryParameters QueryParameters { get; set; }

        [JsonProperty("Score")]
        public double Score { get; set; }
    }

    public partial class QueryParameters
    {
        [JsonProperty("episode")]
        public long Episode { get; set; }

        [JsonProperty("season")]
        public long Season { get; set; }

        [JsonProperty("imdbid")]
        public long Imdbid { get; set; }

        [JsonProperty("sublanguageid")]
        public Sub Sublanguageid { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum LanguageIso639
    {
        [EnumMember(Value = "en")]
        En
    };

    public enum LanguageName { English };

    public enum MatchedBy { Imdbid };

    public enum MovieKind { Episode };

    public enum MovieName { ShadowhuntersTheMortalCup };

    public enum Sub { Eng };

    public enum SubEncoding { Ascii, Utf8 };

    public enum SubFormat { Srt };

    public enum UserNickName { Empty, GoldenBeard };

    public enum UserRank { Administrator, Empty };
    
}
