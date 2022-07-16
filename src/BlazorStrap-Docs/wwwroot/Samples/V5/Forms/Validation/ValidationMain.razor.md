::deep .custom-file-button input[type=file] {
    margin-left: -2px !important;
}

    ::deep .custom-file-button input[type=file]::-webkit-file-upload-button {
        display: none;
    }

    ::deep.custom-file-button input[type=file]::file-selector-button {
        display: none;
    }

::deep .custom-file-button:hover label {
    background-color: #dde0e3;
    cursor: pointer;
}
