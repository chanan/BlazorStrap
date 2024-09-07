<BSTable Color="OTableColor" IsBordered="OIsBordered" IsBorderLess="OIsBorderLess" IsCaptionTop="OIsCaptionTop" IsStriped="OIsStriped">
    <caption>List of users</caption>
    <BSTHead>
        <BSTR>
            <BSTD>#</BSTD>
            <BSTD>First</BSTD>
            <BSTD>Last</BSTD>
            <BSTD>Handle</BSTD>
        </BSTR>
    </BSTHead>
    <BSTBody>
        <BSTR>
            <BSTD AlignRow="OAlignRow" Color="OTDColor" IsActive="OIsActive">1</BSTD>
            <BSTD>John</BSTD>
            <BSTD>Doe</BSTD>
            <BSTD>JohnDoe <br/>media.com</BSTD>
        </BSTR>
        <BSTR AlignRow="OTRAlignRow" Color="OTRColor" IsActive="OTRIsActive">
            <BSTD>2</BSTD>
            <BSTD>Jane</BSTD>
            <BSTD>Doe</BSTD>
            <BSTD>JaneDoe <br/>media.com</BSTD>
        </BSTR>
    </BSTBody>
    <BSTFoot>
        <BSTR>
            <BSTD ColSpan="4">Spanned Table Footer Here</BSTD>
        </BSTR>

    </BSTFoot>
</BSTable>
<!--\\-->
<div class="@BS.Input_Group mb-3">
    <span class="@BS.Input_Group_Text">Table Backgroud Color</span>
    <BSInput InputType="InputType.Select" @bind-Value="OTableColor">
        <option value="@BSColor.Default">None</option>
        <option value="@BSColor.Primary">Primary</option>
        <option value="@BSColor.Secondary">Secondary</option>
        <option value="@BSColor.Success">Success</option>
        <option value="@BSColor.Danger">Danger</option>
        <option value="@BSColor.Warning">Warning</option>
        <option value="@BSColor.Info">Info</option>
        <option value="@BSColor.Light">Light</option>
        <option value="@BSColor.Dark">Dark</option>
    </BSInput>
    <BSInputCheckbox @bind-Value="OIsBordered" IsToggle="true" IsOutlined="true" Color="BSColor.Primary" CheckedValue="true" UnCheckedValue="false">IsBordered</BSInputCheckbox>
    <BSInputCheckbox @bind-Value="OIsBorderLess" IsToggle="true" IsOutlined="true" Color="BSColor.Secondary" CheckedValue="true" UnCheckedValue="false">IsBorderLess</BSInputCheckbox>
    <BSInputCheckbox @bind-Value="OIsCaptionTop" IsToggle="true" IsOutlined="true" Color="BSColor.Success" CheckedValue="true" UnCheckedValue="false">IsCaptionTop</BSInputCheckbox>
    <BSInputCheckbox @bind-Value="OIsStriped" IsToggle="true" IsOutlined="true" Color="BSColor.Danger" CheckedValue="true" UnCheckedValue="false">IsStriped</BSInputCheckbox>
</div>
<div class="@BS.Input_Group mb-3">
    <span class="@BS.Input_Group_Text">Row 2 Backgroud Color</span>
    <BSInput InputType="InputType.Select" @bind-Value="OTRColor">
        <option value="@BSColor.Default">None</option>
        <option value="@BSColor.Primary">Primary</option>
        <option value="@BSColor.Secondary">Secondary</option>
        <option value="@BSColor.Success">Success</option>
        <option value="@BSColor.Danger">Danger</option>
        <option value="@BSColor.Warning">Warning</option>
        <option value="@BSColor.Info">Info</option>
        <option value="@BSColor.Light">Light</option>
        <option value="@BSColor.Dark">Dark</option>
    </BSInput>
    <span class="@BS.Input_Group_Text"> Align</span>
    <BSInput InputType="InputType.Select" @bind-Value="OTRAlignRow">
        <option value="@AlignRow.Default">None</option>
        <option value="@AlignRow.Top">Top</option>
        <option value="@AlignRow.Middle">Middle</option>
        <option value="@AlignRow.Bottom">Bottom</option>
    </BSInput>
    <BSInputCheckbox @bind-Value="OTRIsActive" IsToggle="true" IsOutlined="true" Color="BSColor.Primary" CheckedValue="true" UnCheckedValue="false">TRIsActive</BSInputCheckbox>
</div>
<div class="@BS.Input_Group mb-3">
    <span class="@BS.Input_Group_Text">Row 1 Col 1 Backgroud Color</span>
    <BSInput InputType="InputType.Select" @bind-Value="OTDColor">
        <option value="@BSColor.Default">None</option>
        <option value="@BSColor.Primary">Primary</option>
        <option value="@BSColor.Secondary">Secondary</option>
        <option value="@BSColor.Success">Success</option>
        <option value="@BSColor.Danger">Danger</option>
        <option value="@BSColor.Warning">Warning</option>
        <option value="@BSColor.Info">Info</option>
        <option value="@BSColor.Light">Light</option>
        <option value="@BSColor.Dark">Dark</option>
    </BSInput>
    <span class="@BS.Input_Group_Text"> Align</span>
        <BSInput InputType="InputType.Select" @bind-Value="OAlignRow">
            <option value="@AlignRow.Default">None</option>
            <option value="@AlignRow.Top">Top</option>
            <option value="@AlignRow.Middle">Middle</option>
            <option value="@AlignRow.Bottom">Bottom</option>
        </BSInput>
    <BSInputCheckbox @bind-Value="OIsActive" IsToggle="true" IsOutlined="true" Color="BSColor.Primary" CheckedValue="true" UnCheckedValue="false">TRIsActive</BSInputCheckbox>
</div>
<!--//-->
<!-- Options Form not shown-->
@code {
    // BSTable Parameters
    private BSColor OTableColor { get; set; } = BSColor.Default; // Not Set
    private bool OIsBordered { get; set; } 
    private bool OIsBorderLess { get; set; }
    private bool OIsCaptionTop { get; set; }
    private bool OIsStriped { get; set; }
    
    // BSTR Parameters
    private AlignRow OTRAlignRow { get; set; }
    private BSColor OTRColor { get; set; } = BSColor.Default; // Not Set
    private bool OTRIsActive { get; set; }
    // BSTD Parameters
    private AlignRow OAlignRow { get; set; }
    private BSColor OTDColor { get; set; } = BSColor.Default; // Not Set
    private bool OIsActive { get; set; }
}

