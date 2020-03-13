# Unity UI Unifier (UUU)
Unity UI Unifier (UUU) is a Unity library that unifiedly handles UI components (such as Text and Button).

## Usage
### Attach UnifiedText components
Attach a UnifiedText component to each Text component.

![Attach UnifiedText](https://user-images.githubusercontent.com/4415085/76600119-a694a980-6549-11ea-93a2-7190e11af4e4.png)

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
