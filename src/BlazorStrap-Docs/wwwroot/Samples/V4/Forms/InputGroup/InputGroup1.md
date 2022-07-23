<BSInputGroup MarginBottom="Margins.Medium">
    <BSInputGroup IsPrepend="true">
        <span class="@BS.Input_Group_Text">@@</span>
    </BSInputGroup>
    <BSInput InputType="InputType.Text" placeholder="Username" Value="@("")" />
</BSInputGroup>

<BSInputGroup MarginBottom="Margins.Medium">
    <BSInput InputType="InputType.Text" placeholder="Recipient's username" Value="@("")" />
    <BSInputGroup IsAppend="true">
        <span class="@BS.Input_Group_Text">@@example.com</span>
    </BSInputGroup>
</BSInputGroup>

<BSLabel>Your vanity URL</BSLabel>
<BSInputGroup MarginBottom="Margins.Medium">
    <BSInputGroup IsPrepend="true">
        <span class="@BS.Input_Group_Text" id="basic-addon3">https://example.com/users/</span>
    </BSInputGroup>
    <BSInput InputType="InputType.Text" Value="@("")" />
</BSInputGroup>

<BSInputGroup MarginBottom="Margins.Medium">
    <BSInputGroup IsPrepend="true">
        <span class="@BS.Input_Group_Text">$</span>
    </BSInputGroup>
    <BSInput InputType="InputType.Text" Value="@("")" />
    <BSInputGroup IsAppend="true">
        <span class="@BS.Input_Group_Text">.00</span>
    </BSInputGroup>
</BSInputGroup>

<BSInputGroup MarginBottom="Margins.Medium">
    <BSInputGroup IsPrepend="true">
        <BSInput InputType="InputType.Text" placeholder="Username" Value="@("")" />
    </BSInputGroup>
    <span class="@BS.Input_Group_Text">@@</span>
    <BSInputGroup IsAppend="true">
        <BSInput InputType="InputType.Text" placeholder="Server" Value="@("")" />
    </BSInputGroup>
</BSInputGroup>

<BSInputGroup MarginBottom="Margins.Medium">
    <BSInputGroup IsPrepend="true">
        <span class="@BS.Input_Group_Text">With textarea</span>
    </BSInputGroup>
    <BSInput InputType="InputType.TextArea" Value="@("")"></BSInput>
</BSInputGroup>