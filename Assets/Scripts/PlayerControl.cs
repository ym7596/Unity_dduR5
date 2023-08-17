using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rigid;

    private float horizontal;
    private float vertical;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject bullet2;
    [SerializeField]
    private Transform[] spawnPoint;

    public Transform spawnPoint2;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(horizontal * speed, vertical * speed);

    }
    private void LateUpdate()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //다시 월드 좌표로 변환한다.
        transform.position = worldPos; //좌표를 적용한다.
    }
    IEnumerator Shoot()
    {
        GameObject[] gs;
        for(int i = 0; i < spawnPoint.Length; i++)
        {
            Instantiate(bullet, spawnPoint[i].position, Quaternion.identity);
        }
        Instantiate(bullet2, spawnPoint2.position, Quaternion.identity);
      //  GameObject g = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
        yield return new WaitForEndOfFrame();
    }
    public void Move(InputAction.CallbackContext context)
    {
       
            horizontal = context.ReadValue<Vector2>().x;
            vertical = context.ReadValue<Vector2>().y;
        
    }
    public void Fire(InputAction.CallbackContext context)
    {
       
        if (context.started)
        {
            StartCoroutine(Shoot());
        }
        else { }
    }
}
