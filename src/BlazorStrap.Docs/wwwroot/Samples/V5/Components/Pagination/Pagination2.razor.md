<BSPagination Pages="100" @bind-Value="Page"/>
@Page
@code{
    private int Page { get; set; }= 5;
}