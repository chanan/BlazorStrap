::deep .bs-datagrid {
    height: 20rem;
    overflow-y: auto;
}

::deep .bs-datagrid table{
    min-width: 100%;
}
::deep .bs-datagrid tr {
    height: 30px;
}

.grid-header-button {
    font-weight: inherit;
    background: none;
    color: inherit;
    border: none;
    text-align: inherit;
    margin: 0;
    padding: 0;
}
::deep .grid-header-link{
    font-weight: inherit;
    background: none;
    color: inherit;
    border: none;
    text-align: inherit;
    margin: 0 .25rem 0 0;
    padding: 0;
}
th button.sort-by {
    padding-right: 18px;
    position: relative;
}

button.sort-by:before,
button.sort-by:after {
    border: 4px solid transparent;
    content: "";
    display: block;
    height: 0;
    right: 5px;
    top: 50%;
    position: absolute;
    width: 0;
}

button.sort-by:before {
    border-bottom-color: #666;
    margin-top: -9px;
}

button.sort-by:after {
    border-top-color: #666;
    margin-top: 1px;
}

th button.sort {
    padding-right: 18px;
    position: relative;
}

button.sort:before,
button.sort:after {
    border: 4px solid transparent;
    content: "";
    display: block;
    height: 0;
    right: 5px;
    top: 50%;
    position: absolute;
    width: 0;
}

button.sort:before {
    border-bottom-color: #666;
    margin-top: -9px;
}

th button.sort-desc {
    padding-right: 18px;
    position: relative;
}

button.sort-desc:before,
button.sort-desc:after {
    border: 4px solid transparent;
    content: "";
    display: block;
    height: 0;
    right: 5px;
    top: 50%;
    position: absolute;
    width: 0;
}

button.sort-desc:after {
    border-top-color: #666;
    margin-top: 1px;
}


.system-uicons--filter {
    display: inline-block;
    width: 1em;
    height: 1em;
    --svg: url("data:image/svg+xml,%3Csvg height='21' viewBox='0 0 21 21' width='21' xmlns='http://www.w3.org/2000/svg'%3E%3Cpath d='m.5.5h12l-4 7v3l-3 3v-6z' fill='currentColor' stroke='currentColor' stroke-linecap='round' stroke-linejoin='round' transform='translate(4 4)'/%3E%3C/svg%3E");
    background-color: currentColor;
    -webkit-mask-image: var(--svg);
    mask-image: var(--svg);
    -webkit-mask-repeat: no-repeat;
    mask-repeat: no-repeat;
    -webkit-mask-size: 100% 100%;
    mask-size: 100% 100%;
}
.system-uicons--filter-empty {
    display: inline-block;
    width: 1em;
    height: 1em;
    --svg: url("data:image/svg+xml,%3Csvg height='21' viewBox='0 0 21 21' width='21' xmlns='http://www.w3.org/2000/svg'%3E%3Cpath d='m.5.5h12l-4 7v3l-3 3v-6z' fill='none' stroke='currentColor' stroke-linecap='round' stroke-linejoin='round' transform='translate(4 4)'/%3E%3C/svg%3E");
    background-color: currentColor;
    -webkit-mask-image: var(--svg);
    mask-image: var(--svg);
    -webkit-mask-repeat: no-repeat;
    mask-repeat: no-repeat;
    -webkit-mask-size: 100% 100%;
    mask-size: 100% 100%;
}
.system-uicons--menu {
    display: inline-block;
    width: 1em;
    height: 1em;
    --svg: url("data:image/svg+xml,%3Csvg height='21' viewBox='0 0 21 21' width='21' xmlns='http://www.w3.org/2000/svg'%3E%3Cg fill='none' fill-rule='evenodd' stroke='currentColor' stroke-linecap='round' stroke-linejoin='round'%3E%3Cpath d='m4.5 6.5h12'/%3E%3Cpath d='m4.498 10.5h11.997'/%3E%3Cpath d='m4.5 14.5h11.995'/%3E%3C/g%3E%3C/svg%3E");
    background-color: currentColor;
    -webkit-mask-image: var(--svg);
    mask-image: var(--svg);
    -webkit-mask-repeat: no-repeat;
    mask-repeat: no-repeat;
    -webkit-mask-size: 100% 100%;
    mask-size: 100% 100%;
}

