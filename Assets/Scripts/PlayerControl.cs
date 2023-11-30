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
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); //ĳ������ ���� ��ǥ�� ����Ʈ ��ǥ��� ��ȯ���ش�.
        viewPos.x = Mathf.Clamp01(viewPos.x); //x���� 0�̻�, 1���Ϸ� �����Ѵ�.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y���� 0�̻�, 1���Ϸ� �����Ѵ�.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //�ٽ� ���� ��ǥ�� ��ȯ�Ѵ�.
        transform.position = worldPos; //��ǥ�� �����Ѵ�.
    }
    IEnumerator Shoot()
    {
        GameObject[] gs;
        for(int i = 0; i < spawnPoint.Length; i++)
        {
           GameObject temp =  Instantiate(bullet, spawnPoint[i].position, Quaternion.identity);
           Vector3 direction = new Vector2(Mathf.Cos((20+20*i)*Mathf.Deg2Rad), Mathf.Sin((20+20*i)*Mathf.Deg2Rad));

           temp.transform.right = direction;
           temp.transform.position = spawnPoint[i].position + direction;
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
