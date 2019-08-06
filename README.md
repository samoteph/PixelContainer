# PixelContainer

PixelContainer is a control that allows you to display an image (with Stretch attribute) and then add child controls that can be positioned and sized using the pixel as a unit. Very handy for Bitmap oriented applications !


```
<my:PixelContainer Source="Assets/Mario.png" Stretch="Uniform">
  <Button my:Pixel.X="100" my:Pixel.Y="100" my:Pixel.Width="200" my:Pixel.Height="80" HorizontalAlignement="Stretch" VerticalAlignement="Stretch" Content="Hello" />
</my:PixelContainer>
```

In this example, the Pixel Container show the image "Mario.png" then add a Button at the position 100,100 and a size of 200,80 in pixels.

For more examples and a complete description of PixelContainer, follow this link :

http://samuelblanchard.com/2019/08/01/donner-vie-a-vos-images-avec-pixelcontainer

Compatibility : UWP/WPF
