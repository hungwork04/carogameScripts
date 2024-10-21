
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RanDomMeme : MonoEdited
{
    [SerializeField] protected List<Sprite> sprites=new List<Sprite>();
    [Header("MEME")]
    public Sprite a, b, c, d, e, f, g, h, i, k, l, m;
    [Header("Image")]
    public Image meme;

    protected override void Awake()
    {
        base.Start();
        meme = GetComponent<Image>();
        sprites.Add(a);
        sprites.Add(b);
        sprites.Add(c);
        sprites.Add(d);
        sprites.Add(e);
        sprites.Add (f);
        sprites.Add(g);
        sprites.Add(h);
        sprites.Add(i);
        sprites.Add(k);
        sprites.Add(l);
        sprites.Add(m);
    }

    public virtual void GetRandomMEME()
    {
        int rand = Random.Range(0, sprites.Count);
        meme.sprite = this.sprites[rand];
    }
}
