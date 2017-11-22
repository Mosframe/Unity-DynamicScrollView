/**
 * RealTimeInsertItemExample.cs
 *
 * @author mosframe / https://github.com/mosframe
 *
 */

namespace Mosframe {

    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// RealTimeInsertItemExample
    /// </summary>
    public class RealTimeInsertItemExample : MonoBehaviour {

        public class CustomData {

            public string   name;
            public int      value;
        }

        public static List<CustomData> data = new List<CustomData>();



        public DynamicScrollView    scrollView;
        public  int                 dataIndex    = 1;
        public  string              dataName     = "Insert01";



        private void Start() {

            this.insertItem( 0, new CustomData(){name="data00", value=100} );
            this.insertItem( 0, new CustomData(){name="data01", value=100} );
        }

        public void insertItem( int index, CustomData data ) {

            // TODO : set custom data
            RealTimeInsertItemExample.data.Insert( index, data );

            this.scrollView.totalItemCount = RealTimeInsertItemExample.data.Count;
        }

    }

    #if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(RealTimeInsertItemExample))]
    public class RealTimeAddItemExampleEditor : UnityEditor.Editor {

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if( Application.isPlaying ) {

                if( GUILayout.Button("Insert") ) {

                    var example = (RealTimeInsertItemExample)this.target;
                    example.insertItem(example.dataIndex, new RealTimeInsertItemExample.CustomData(){name=example.dataName,value=100} );
                }
            }
        }
    }

    #endif
}