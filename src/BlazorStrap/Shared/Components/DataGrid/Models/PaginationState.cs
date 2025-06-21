using Microsoft.AspNetCore.Components;

namespace BlazorStrap;

public abstract class PaginationStateBase
{
    public int CurrentPage { get; set; } = 1;
    private int _itemsPerPage;
    private int _lastItemsPerPage;
    public int ItemsPerPage
    {
        get => _itemsPerPage;
        set
        {
            _itemsPerPage = value;
            if(_lastItemsPerPage != value)
            {
                CurrentPage = TryToKeepUserOnRightPage(value, _lastItemsPerPage, CurrentPage, TotalItems ?? 0, TotalPages);
                _lastItemsPerPage = value;
            }
            
            if(ItemPerPageChanged.HasDelegate)
                _= ItemPerPageChanged.InvokeAsync(value);
            _= OnStateChange?.Invoke(this);
        }
    }

    private static int TryToKeepUserOnRightPage(int newItemsPerPage, int lastItemsPerPage, int currentPage, int totalItems, int totalPages)
    {
        if (totalItems <= 0) return 1;
        
        var percentageThroughItems = (currentPage * lastItemsPerPage) / (double)totalItems;
        currentPage = (int)Math.Ceiling((percentageThroughItems * (double)totalItems / newItemsPerPage));

        //Keep the current page within the bounds of the new total pages
        if (currentPage > totalPages)
        {
            return totalPages;
        }
        else if (currentPage < 1)
        {
            return 1;
        }
        return currentPage;
    }

    public EventCallback<int> ItemPerPageChanged { get; set; }
    public int? TotalItems { get; set; }
    private Placement _placement = Placement.Bottom;

    public Placement Placement
    {
        get => _placement;
        set
        {
            _placement = value;
            if(PlacementIsLeft || PlacementIsRight)
                _placement = Placement.Bottom;
        }
    }

    public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalItems ?? 0, ItemsPerPage));
    public bool PlacementIsTop => Placement == Placement.Top || Placement == Placement.TopStart || Placement == Placement.TopEnd;
    public bool PlacementIsBottom => Placement == Placement.Bottom || Placement == Placement.BottomStart || Placement == Placement.BottomEnd;
    private bool PlacementIsLeft => Placement == Placement.Left || Placement == Placement.LeftStart || Placement == Placement.LeftEnd;
    private bool PlacementIsRight => Placement == Placement.Right || Placement == Placement.RightStart || Placement == Placement.RightEnd;
    
    public string PlacementClass => Placement switch
    {
        Placement.Top => "justify-content-center",
        Placement.TopStart => "justify-content-start",
        Placement.TopEnd => "justify-content-end",
        Placement.Bottom => "justify-content-center",
        Placement.BottomStart => "justify-content-start",
        Placement.BottomEnd => "justify-content-end",
        _ => "pagination-bottom"
    };
    public Task GoToPageAsync(int page)
    {
        CurrentPage = page;
        return OnStateChange?.Invoke(this) ?? Task.CompletedTask;
    }
    public Task GoToNextPageAsync()
    {
        CurrentPage = CurrentPage + 1;
        return OnStateChange?.Invoke(this) ?? Task.CompletedTask;
    }
    public Task GoToPreviousPageAsync()
    {
        CurrentPage = CurrentPage - 1;
        return OnStateChange?.Invoke(this) ?? Task.CompletedTask;
    }
    public Task GoToFirstPageAsync()
    {
        CurrentPage = 1;
        return OnStateChange?.Invoke(this) ?? Task.CompletedTask;
    }
    public Task GoToLastPageAsync()
    {
        CurrentPage = TotalPages;
        return OnStateChange?.Invoke(this) ?? Task.CompletedTask;
    }
    internal Func<PaginationStateBase, Task>? OnStateChange { get; set; }
}