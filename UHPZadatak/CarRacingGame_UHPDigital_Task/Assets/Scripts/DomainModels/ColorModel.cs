public class ColorModel
{
    public enum BodyColor
    {
        White = 0,
        Black = 1,
        Red = 2,
        Green = 3
    }

    public BodyColor BodyColorSelection { get; set; }

    public ColorModel(BodyColor color)
    {
        this.BodyColorSelection = color;
    }

}
