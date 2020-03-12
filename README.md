# Unity UI Unifier (UUU)
Unity UI Unifier (UUU) is a Unity library that unifiedly handles UI components (such as Text and Button).

# Usage (Text component)
## Attach UnifiedText components
Attach a UnifiedText component to each Text components.

写真


## Reference Text components
When referencing text components, use UnifiedText instead of concrete class (e.g. Text and TextMesh).

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

# Author
Furuta, Yusuke ([@tarukosu](https://twitter.com/tarukosu))

# License
MIT
