using UnityEngine;
using cakeslice;

public class RaycastHighlighter : MonoBehaviour
{
    public float highlightDistance = 5f;
    private Outline currentOutline;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, highlightDistance))
        {
            Outline outline = hit.transform.GetComponent<Outline>();

            if (outline != null)
            {
                // Jeœli zmieni³ siê obiekt
                if (outline != currentOutline)
                {
                    ClearOutline();
                    currentOutline = outline;
                    currentOutline.enabled = true;
                }
                return;
            }
        }

        ClearOutline();
    }

    void ClearOutline()
    {
        if (currentOutline != null)
        {
            currentOutline.enabled = false;
            currentOutline = null;
        }
    }
}
