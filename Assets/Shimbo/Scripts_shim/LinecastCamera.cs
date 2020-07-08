using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinecastCamera : MonoBehaviour
{
    Vector3 vec;

    public Transform LookAtTargetPos;

    public float cameraMoveSpeed;

    public LayerMask objLayer;//障害物のレイヤー

    // Start is called before the first frame update
    void Start()
    {
        //カメラ位置
        vec = LookAtTargetPos.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //カメラの位置をキャラクターの後ろ側に移動する(滑らかに移動)
        transform.position = Vector3.Lerp(transform.position, LookAtTargetPos.position - vec, cameraMoveSpeed * Time.deltaTime);

        RaycastHit hit;
        //キャラクターとカメラの位置に障害物があったら障害物の位置にカメラを移動させる
        if(Physics.Linecast(LookAtTargetPos.position, transform.position, out hit, objLayer))
        {
            //障害物の中身が見えないように一気に衝突ポイントに移動する
            transform.position = hit.point;
        }

        //レイを視覚的に確認
        Debug.DrawLine(LookAtTargetPos.position, transform.position, Color.red, 0f, false);

        // カメラの回転ではQuaternion.Slerpを使って『現在の角度』から『カメラからキャラクターのカメラが見る位置の方向の角度』へと徐々に回転
        //　スピードを考慮しない場合はLookAtで出来る
        transform.LookAt(LookAtTargetPos.position);

        //　スピードを考慮する場合
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAtTargetPos.position - transform.position), cameraRotateSpeed * Time.deltaTime);

    }
}
