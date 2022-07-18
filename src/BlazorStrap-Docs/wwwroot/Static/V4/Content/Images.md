## Images
#### Component \<BSImage\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter     | Type   | Valid      | Remarks/Output           | 
|---------------|--------|------------|--------------------------|
| Align         | Enum   | Align      |                          | {.table-striped .p-2}  
| IsFluid       | bool   | true/false | `.img-fluid`             |
| IsRounded     | bool   | true/false | `.rounded`               |
| IsPlaceholder | bool   | true/false | Outputs SVG place holder |
| IsThumbnail   | bool   | true/false | `.img-thumbnail`         |
| Source        | string | string     |                          |

:::

#### Component \<BSSvg\>


| Parameter     | Type   | Valid      | Remarks/Output           | 
|---------------|--------|------------|--------------------------|
| Align         | Enum   | Align      |                          | {.table-striped .p-2}  
| IsFluid       | bool   | true/false | `.img-fluid`             |
| IsRounded     | bool   | true/false | `.rounded`               |
| IsThumbnail   | bool   | true/false | `.img-thumbnail`         |
| Source        | string | string     |                          |

`<BSSvg>` Directly injects the SVG into the page. Useful for animating Svg's

All examples use our new placeholder switch `IsPlaceholder`

### Responsive images
{{sample=V4/Content/Images/Images1}}

### Image thumbnails

{{sample=V4/Content/Images/Images2}}

### Aligning images

{{sample=V4/Content/Images/Images3}}

{{sample=V4/Content/Images/Images4}}

{{sample=V4/Content/Images/Images5}}

### Svg Loader
Note: CSS Isolation will not work.  
{{sample=V4/Content/Images/Images6;;CSS}}