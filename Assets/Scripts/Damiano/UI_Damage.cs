using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Damage : MonoBehaviour {

    public AnimationCurve curve;
    public int damage = 100;
    float time;

    public GameObject ui_damage, bg_damage;
    Text ui_dmg, bg_dmg;

	// Use this for initialization
	void Start () {
        time = Time.time;

        ui_dmg = ui_damage.GetComponent<Text>();
        bg_dmg = bg_damage.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        ui_dmg.text = "" + damage;
        bg_dmg.text = "" + damage;


        transform.LookAt(Camera.main.transform, transform.up);
        transform.Rotate(new Vector3(0, 180, 0));

        float past = Time.time - time;

        float alpha = curve.Evaluate(past);

        ui_dmg.color = new Color(ui_dmg.color.r, ui_dmg.color.g, ui_dmg.color.b , alpha);
        bg_dmg.color = new Color(ui_dmg.color.r, ui_dmg.color.g, ui_dmg.color.b, alpha);

        transform.position += Vector3.up * Time.deltaTime * 0.5f;

        Keyframe lastframe = curve[curve.length - 1];

        if (lastframe.time < past) {
            Destroy(gameObject);
        }
    }
}
