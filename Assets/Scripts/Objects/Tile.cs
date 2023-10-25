using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    [Header("Values")]
    public float speed;
    public int ID;

    [Header("Status")]
    public TileState state;

    [Header("Face")]
    public MeshRenderer faceMesh;

    private Node _target;
    private Vector3 _targetPosition;

    // Update is called once per frame
    void Update()
    {
        if (MasterInstance.GameManager.state != GameState.PLAYING)
        {
            return;
        }
        if (state == TileState.MOVING && _target != null)
        {
            if (Vector3.Distance(transform.position, _targetPosition) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, _targetPosition, speed * Time.deltaTime);
            }
            else
            {
                transform.position = _targetPosition;
                state = TileState.DONE;
                MasterInstance.AudioManager.PlayEffect(AUDIO.PLACED);
                MasterInstance.GameManager.nodeContainer.CheckNode(ID);
            }
        }
    }

    public void SetFace(Material material)
    {
        faceMesh.material = material;
    }

    public void SetSprite(Sprite sprite)
    {
        //Debug.Log(faceMaterial == null ? "NULL" : "NO");
        faceMesh.material.SetTexture("_MainTex", sprite.texture);
    }

    public void SetTargetAndMove(Node node)
    {
        _target = node;
        _targetPosition = _target.transform.position;
        _targetPosition.y = 0.05f;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (state != TileState.CAN_INTERACT || MasterInstance.GameManager.state != GameState.PLAYING)
        {
            return;
        }
        MasterInstance.GameManager.MoveTileToNode(this);
        // disable physics
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        state = TileState.MOVING;
    }

    public void ResetTile()
    {
        state = TileState.CAN_INTERACT;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;

    }

    public void ShowTile(bool show)
    {
        gameObject.SetActive(show);
    }

    public void SetTileState(TileState state)
    {
        this.state = state;
        switch (state)
        {
            case TileState.DONE:
                gameObject.SetActive(false);
                break;
            case TileState.CAN_INTERACT:
                gameObject.SetActive(true);
                break;
            case TileState.NONE:
                gameObject.SetActive(true);
                break;
        }
    }
}
