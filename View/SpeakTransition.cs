﻿//======================================================================
//
//       CopyRight 2019-2020 © MUXI Game Studio 
//       . All Rights Reserved 
//
//        FileName :  SpeakTransition.cs
//
//        Created by 半世癫(Roc) at 2021-02-04 21:41:56
//
//======================================================================
using System;
using System.Collections;
using GalForUnity.Attributes;
using GalForUnity.System.Event;
using UnityEngine;
using UnityEngine.UI;

namespace GalForUnity.View{
    /// <summary>
    /// 一个线性过渡UI语句的解决方案的解决方案
    /// 将此类挂在，并将语句视图赋值，即可平滑过渡视图元素
    /// </summary>
    public class SpeakTransition : MonoBehaviour{
        [Rename(nameof(intervalTime))]
        public float intervalTime = 0.1f;
        [Rename(nameof(speakView))]
        public Text speakView;
        private string _currentSpeak;
        private int _currentPoint;
        private bool _isOnTransition;
        // private float _lastTime = 0;

        private void Awake(){
            if (!speakView)
                if (!TryGetComponent<Text>(out speakView))
                    throw new NullReferenceException("找不到语句的输出视图！");
            EventCenter.GetInstance().OnSpeak = OnSpeak;
        }

        private string OnSpeak(string speak){
            if (_isOnTransition){
                StopCoroutine(TransitionSpeak());
                _isOnTransition = false;
                this.speakView.text = "";
            }
            _currentSpeak = speak;
            _currentPoint = 0;
            StartCoroutine(TransitionSpeak());
            return "";
        }
        
        private IEnumerator TransitionSpeak(){
            _isOnTransition = true;
            yield return new WaitForSeconds(intervalTime);
            if (!string.IsNullOrEmpty(_currentSpeak)){
                while (_currentPoint < _currentSpeak.Length){
                    speakView.text += _currentSpeak[_currentPoint++];
                    yield return new WaitForSeconds(intervalTime);
                }
            }
        }
        
    }
}