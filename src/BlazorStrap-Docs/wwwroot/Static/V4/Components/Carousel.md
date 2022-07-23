## Carousel
#### Component \<BSCarousel\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter     | Type | Valid      | Remarks/Output   | 
|---------------|------|------------|------------------|
| HasIndicators | bool | true/false | Shows Indicators | {.table-striped}   
| IsDark        | bool | true/false | `.carousel-dark` |
| IsFade        | bool | true/false | `.carousel-fade` |
| IsSlide       | bool | true/false | default on       |

:::

{.mt-4}
#### Component \<BSCarouselItem\>
:::

| Parameter | Type | Valid       | Remarks/Output | 
|-----------|------|-------------|----------------|
| Interval  | int  | > 1000 or 0 | 0 is disabled  | {.table-striped}     

:::
#### Component \<BSCarouselCaption\>
No Setting parameters

### Slides only

{{sample=V4/Components/Carousel/Carousel1}}

### With controls

{{sample=V4/Components/Carousel/Carousel2}}

### With indicators

{{sample=V4/Components/Carousel/Carousel3}}

### With captions

{{sample=V4/Components/Carousel/Carousel4}}

### Crossfade

{{sample=V4/Components/Carousel/Carousel5}}

### Individual interval

{{sample=V4/Components/Carousel/Carousel6}}

### Dark variant

{{sample=V4/Components/Carousel/Carousel8}}

### Methods / Events
TValue = BSCarouselItem
:::

| Name        | Type   | Return Value | Remarks                      |
|-------------|--------|--------------|------------------------------|
| ToggleAsync | Method |              | Toggles                      |
| ShowAsync   | Method |              | Shows                        |
| HideAsync   | Method |              | Hides                        |
| OnShow      | Event  | TValue       | Raised when starting to show |
| OnShown     | Event  | TValue       | Raised when shown            |
| OnHide      | Event  | TValue       | Raised when starting to hide |
| OnHidden    | Event  | TValue       | Raised when hidden           |
:::