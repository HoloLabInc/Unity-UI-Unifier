# Unity UI Unifier (UUU)
Unity UI Unifier (UUU) is a Unity library that unifiedly handles UI components (such as Text and Button).

## Usage
### Attach UnifiedText components
Attach a UnifiedText component to each Text component.

![Attach UnifiedText](https://user-images.githubusercontent.com/4415085/76599500-857f8900-6548-11ea-88b8-afe23a38c29e.png)


### Reference Text components
When referencing text components, use UnifiedText instead of concrete classes (e.g. Text and TextMesh).

```c#
public class UpdateTextSample : MonoBehaviour
{
    [SerializeField]
    UnifiedText text = null;

    void Start()
    {
        text.Text = "some text";
    }
}
```

### Button
UnifiedButton can be used in the same way as UnifiedText.  
Attach UnifiedButton to each button and reference UnifiedButton.

## Author
Furuta, Yusuke ([@tarukosu](https://twitter.com/tarukosu))

## License
MIT
