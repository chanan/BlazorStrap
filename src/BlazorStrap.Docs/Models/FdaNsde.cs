using System.Text.Json.Serialization;

namespace BlazorStrap_Docs.Models;

public class Meta
{
    [JsonPropertyName("disclaimer")]
    public string Disclaimer { get; set; }

    [JsonPropertyName("terms")]
    public string Terms { get; set; }

    [JsonPropertyName("license")]
    public string License { get; set; }

    [JsonPropertyName("last_updated")]
    public string LastUpdated { get; set; }

    [JsonPropertyName("results")]
    public MetaResult Results { get; set; }
}

public class MetaResult
{
    [JsonPropertyName("skip")]
    public int Skip { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}

public class FdaNsde
{
    [JsonPropertyName("proprietary_name")] 
    public string ProprietaryName { get; set; }

    [JsonPropertyName("application_number_or_citation")]
    public string ApplicationNumberOrCitation { get; set; }

    [JsonPropertyName("product_type")]
    public string ProductType { get; set; }

    [JsonPropertyName("marketing_start_date")]
    public string MarketingStartDate { get; set; }

    [JsonPropertyName("package_ndc")] 
    public string PackageNdc { get; set; }

    [JsonPropertyName("marketing_category")]
    public string MarketingCategory { get; set; }

    [JsonPropertyName("package_ndc11")] 
    public string PackageNdc11 { get; set; }

    [JsonPropertyName("dosage_form")] 
    public string DosageForm { get; set; }

    [JsonPropertyName("inactivation_date")]
    public string InactivationDate { get; set; }

    [JsonPropertyName("billing_unit")] 
    public string BillingUnit { get; set; }
}

public class FdaNsdeResult
{
    [JsonPropertyName("meta")]
    public Meta Meta { get; set; }

    [JsonPropertyName("results")]
    public List<FdaNsde> Results { get; set; }
}