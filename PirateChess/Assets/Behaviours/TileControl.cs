using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControl : MonoBehaviour
{
    [Header("Child References")]
    [SerializeField] GameObject m_island;
    [SerializeField] ParticleSystem m_fog;

    Vector3 m_BasePos;
    Vector3 m_BaseEuler;

    [Header("Variables - Animated Update")]
    bool m_animating = false;
    bool m_animatingHalf = false;
    bool m_animatingHalfRot = false;
    Vector3 m_raiseAmount = new Vector3(0, 2.5f, 0);

    private void Awake()
    {
        setPos(transform.position);
        setEuler(transform.eulerAngles);
    }

    public void setPos(Vector3 _pos)
    {
        m_BasePos = _pos;
    }
    public void setEuler(Vector3 _euler)
    {
        m_BaseEuler = _euler;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            UpdateTile(true);
    }

    public void UpdateTile(bool _animated = false)
    {
        if (_animated)
            AnimateUpdateTile();
    }

    void AnimateUpdateTile()
    {
        if (!m_animating)
        {
            m_animating = true;
            AnimateMovement(m_BasePos + m_raiseAmount, 0.3f);
            AnimateRotation(Vector3.up * 0.5f, 0.3f);

            if (!m_island.activeSelf)
            {
                m_fog.Stop();
                ParticleSystem.MainModule main = m_fog.main;
                main.simulationSpeed = 3;
            }
        }

        // Update info here
    }

    void AnimateMovement(Vector3 _targPos, float _time, iTween.EaseType _easeType = iTween.EaseType.easeOutBack)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", transform.position,
            "to", _targPos,
            "time", _time,
            "easetype", _easeType,
            "onupdatetarget", gameObject,
            "onupdate", "UpdateTilePosition",
            "oncompletetarget", gameObject,
            "oncomplete", "CompletedMoving"));
    }

    void UpdateTilePosition(Vector3 _position)
    {
        transform.position = _position;
    }

    void CompletedMoving()
    {
        if (!m_animatingHalf)
        {
            AnimateMovement(m_BasePos, 0.3f, iTween.EaseType.easeInBack);
            m_animatingHalf = true;
        }
        else
        {
            m_animatingHalf = false;
            m_animating = false;
        }
    }

    void AnimateRotation(Vector3 _targRot, float _time, iTween.EaseType _easeType = iTween.EaseType.easeInOutBack)
    {
        iTween.RotateBy(gameObject, iTween.Hash(
            "amount", _targRot,
            "time", _time,
            "easetype", _easeType,
            "onupdatetarget", gameObject,
            "onupdate", "UpdateTileRotation",
            "oncompletetarget", gameObject,
            "oncomplete", "CompletedRotating"));
    }

    void UpdateTileRotation()
    {
    }

    void UpdateTileRotation(Vector3 _euler = new Vector3())
    {
        transform.eulerAngles = _euler;
    }

    void CompletedRotating()
    {
        if (!m_animatingHalfRot)
        {
            AnimateRotation(Vector3.up * 0.49f, 0.3f, iTween.EaseType.easeInOutBack);
            m_animatingHalfRot = true;
            // Update display here

            m_island.SetActive(!m_island.activeSelf);
        }
        else
        {
            transform.eulerAngles = m_BaseEuler;
            m_animatingHalfRot = false;
            m_animating = false;
        }
    }

}
