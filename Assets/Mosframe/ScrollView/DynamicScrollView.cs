/**
 * DynamicScrollView.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

 namespace Mosframe {

    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    
    /// <summary>
    /// Dynamic Scroll View
    /// </summary>
    [RequireComponent(typeof(ScrollRect))]
    public abstract class DynamicScrollView : UIBehaviour
    {
        /// <summary> Scroll Direction </summary>
	    public enum Direction {
            Vertical,
            Horizontal,
        }

	    public int                              totalItemCount          = 99;
	    public RectTransform                    itemPrototype           = null;

        protected abstract float                contentAnchoredPosition { get; set; }
	    protected abstract float                contentSize             { get; }
	    protected abstract float                viewportSize            { get; }

        protected Direction                     _direction              = Direction.Vertical;
        protected LinkedList<RectTransform>     _containers             = new LinkedList<RectTransform>();
        protected float                         _prevAnchoredPosition   = 0;
	    protected int                           _nextInsertItemNo       = 0; // 다음 삽입할 아이템 인덱스 ( 뷰포트 상단/좌측 기준 )
        protected float                         _itemSize               = -1;
	    protected int                           _prevTotalItemCount     = 99;
        protected RectTransform                 _viewportRect           = null;
        protected RectTransform                 _contentRect            = null;

        // Awake

        protected override void Awake() {

            if( this.itemPrototype == null ) {
                Debug.LogError( RichText.red(new{this.name,this.itemPrototype}) );
                return;
            }

            base.Awake();

            var scrollRect      = this.GetComponent<ScrollRect>();
            this._viewportRect  = scrollRect.viewport;
            this._contentRect   = scrollRect.content;
        }

        // Start

        protected override void Start ()
	    {
            this.itemPrototype.gameObject.SetActive(false);

            this._prevTotalItemCount = this.totalItemCount;

            // instantiate items
            var itemCount = (int)(this.viewportSize / this._itemSize) + 3;
		    for( var i = 0; i < itemCount; ++i ) {
			    var itemRect = Instantiate( this.itemPrototype );
			    itemRect.SetParent( this._contentRect, false );
			    itemRect.name = i.ToString();
			    itemRect.anchoredPosition = this._direction == Direction.Vertical ? new Vector2(0, -this._itemSize * i) : new Vector2( this._itemSize * i, 0);
                this._containers.AddLast( itemRect );

			    itemRect.gameObject.SetActive( true );

				this.updateItem( i, itemRect.gameObject );
		    }

            // resize content
			this.resizeContent();
	    }

        // update

	    private void Update()
	    {
            if( this.totalItemCount != this._prevTotalItemCount ) {

                this._prevTotalItemCount = this.totalItemCount;

                // check scroll bottom
                var isBottom = false;
                if( this.viewportSize-this.contentAnchoredPosition >= this.contentSize-this._itemSize*0.5f ) {
                    isBottom = true;
                }

                this.resizeContent();

                // move scroll to bottom
                if( isBottom ) {
                    this.contentAnchoredPosition = this.viewportSize - this.contentSize;
                }

                refresh();
            }


            // [ Scroll up ]

		    while( this.contentAnchoredPosition - this._prevAnchoredPosition  < -this._itemSize * 2 ) {

                this._prevAnchoredPosition -= this._itemSize;

                // move a first item to last
                var item = this._containers.First.Value;
                this._containers.RemoveFirst();
                this._containers.AddLast(item);

                // set item position
                var pos = this._itemSize * ( this._containers.Count + this._nextInsertItemNo );
			    item.anchoredPosition = (this._direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

                // update item
                this.updateItem( this._containers.Count+this._nextInsertItemNo, item.gameObject );

			    this._nextInsertItemNo++;
		    }

            // [ Scroll down ]

            while ( this.contentAnchoredPosition - this._prevAnchoredPosition > 0 ) {

                this._prevAnchoredPosition += this._itemSize;

                // move a last item to first
			    var item = this._containers.Last.Value;
                this._containers.RemoveLast();
                this._containers.AddFirst(item);

                this._nextInsertItemNo--;

                // set item position
			    var pos = this._itemSize * this._nextInsertItemNo;
			    item.anchoredPosition = (this._direction == Direction.Vertical) ? new Vector2(0,-pos): new Vector2(pos,0);

                // update item
                this.updateItem( this._nextInsertItemNo, item.gameObject );
		    }
	    }
                
        public void insertItem () {

        }


        // refresh

        private void refresh() {

            var index = 0;
            if( this.contentAnchoredPosition != 0 ) {
                index = (int)(-this.contentAnchoredPosition / this._itemSize);
            }

            foreach( var itemRect in  this._containers ) {

                // set item position
                var pos = this._itemSize * index;
			    itemRect.anchoredPosition = (this._direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

                this.updateItem( index, itemRect.gameObject );

                ++index;
            }

            this._nextInsertItemNo = index - this._containers.Count;
            this._prevAnchoredPosition = (int)(this.contentAnchoredPosition / this._itemSize) * this._itemSize;
        }

        // resize content

        private void resizeContent() {

            var size = this._contentRect.getSize();
            if( this._direction == Direction.Vertical ) size.y = this._itemSize * this.totalItemCount;
            else                                        size.x = this._itemSize * this.totalItemCount;
            this._contentRect.setSize( size );
        }

        // update item

	    private void updateItem( int index, GameObject itemObj )
	    {
		    if( index < 0 || index >= this.totalItemCount ) {

			    itemObj.SetActive(false);
		    }
		    else {

			    itemObj.SetActive(true);
			
			    var item = itemObj.GetComponent<IDynamicScrollViewItem>();
                if( item != null ) item.onUpdateItem( index );
		    }
	    }



        #region [ Editor ]

        #if UNITY_EDITOR

        // Reset
        protected override void Reset() {
            base.Reset();

            // [ cliear ]

            while( this.transform.childCount>0 ) {
                DestroyImmediate( this.transform.GetChild(0).gameObject );
            }

            // [ RectTransform ]

            var rectTransform = this.GetComponent<RectTransform>();
            rectTransform.setFullSize();

            // [ ScrollRect ]

            var scrollRect = this.GetComponent<ScrollRect>();
            scrollRect.horizontal   = this._direction == Direction.Horizontal;
            scrollRect.vertical     = this._direction == Direction.Vertical;

            // [ ScrollRect / Viewport ]

            var viewportRect = new GameObject( "Viewport", typeof(RectTransform), typeof(Mask), typeof(Image) ).GetComponent<RectTransform>();
            viewportRect.SetParent( scrollRect.transform, false );
            viewportRect.setFullSize();
            viewportRect.offsetMin = new Vector2( 10f, 10f );
            viewportRect.offsetMax = new Vector2(-10f,-10f );
            var viewportImage = viewportRect.GetComponent<Image>();
            //viewportImage.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            viewportImage.type = Image.Type.Sliced;
            var viewportMask = viewportRect.GetComponent<Mask>();
            viewportMask.showMaskGraphic = false;
            scrollRect.viewport = viewportRect;

            // [ ScrollRect / Viewport / Content ]

            var contentRect = new GameObject( "Content", typeof(RectTransform) ).GetComponent<RectTransform>();
            contentRect.SetParent( viewportRect, false );
            if( this._direction == Direction.Horizontal ) contentRect.setSizeFromLeft( 1.0f ); else contentRect.setSizeFromTop( 1.0f );
            var contentRectSize = contentRect.getSize();
            contentRect.setSize( contentRectSize-contentRectSize*0.06f );
            scrollRect.content = contentRect;

            // [ ScrollRect / Viewport / Content / PrototypeItem ]

            this.resetPrototypeItem( contentRect );


            // [ ScrollRect / Scrollbar ]

            var scrollbarName = this._direction == Direction.Horizontal ? "Scrollbar Horizontal" : "Scrollbar Vertical";
            var scrollbarRect = new GameObject( scrollbarName, typeof(Scrollbar), typeof(Image) ).GetComponent<RectTransform>();
            scrollbarRect.SetParent( viewportRect, false );
            if( this._direction == Direction.Horizontal ) scrollbarRect.setSizeFromBottom( 0.05f ); else scrollbarRect.setSizeFromRight( 0.05f );
            scrollbarRect.SetParent( scrollRect.transform, true );

            var scrollbar = scrollbarRect.GetComponent<Scrollbar>();
            scrollbar.direction = ( this._direction == Direction.Horizontal ) ? Scrollbar.Direction.LeftToRight : Scrollbar.Direction.BottomToTop;
            if( this._direction == Direction.Horizontal ) scrollRect.horizontalScrollbar = scrollbar; else scrollRect.verticalScrollbar = scrollbar;

            // [ ScrollRect / Scrollbar / Image ]

            var scrollbarImage = scrollbarRect.GetComponent<Image>();
            scrollbarImage.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
            scrollbarImage.type = Image.Type.Sliced;

            // [ ScrollRect / Scrollbar / slidingArea ]

            var slidingAreaRect = new GameObject( "Sliding Area", typeof(RectTransform) ).GetComponent<RectTransform>();
            slidingAreaRect.SetParent( scrollbarRect, false );
            slidingAreaRect.setFullSize();

            // [ ScrollRect / Scrollbar / slidingArea / Handle ]

            var scrollbarHandleRect = new GameObject( "Handle", typeof(Image) ).GetComponent<RectTransform>();
            scrollbarHandleRect.SetParent( slidingAreaRect, false );
            scrollbarHandleRect.setFullSize();
            var scrollbarHandleImage = scrollbarHandleRect.GetComponent<Image>();
            scrollbarHandleImage.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            scrollbarHandleImage.type   = Image.Type.Sliced;
            scrollbar.handleRect = scrollbarHandleRect;

            // [ ScrollRect / ScrollbarHandleSize ]

            var scrollbarHandleSize = scrollRect.GetComponent<ScrollbarHandleSize>();
            if( scrollbarHandleSize == null ) {
                scrollbarHandleSize = scrollRect.gameObject.AddComponent<ScrollbarHandleSize>();
                scrollbarHandleSize.maxSize = 1.0f;
                scrollbarHandleSize.minSize = 0.1f;
            }

            // [ Layer ]

            this.gameObject.setLayer( this.transform.parent.gameObject.layer, true );
        }

        // reset prototype item 

        protected virtual void resetPrototypeItem( RectTransform contentRect ) {

            // [ ScrollRect / Viewport / Content / PrototypeItem ]

            var prototypeItemRect = new GameObject( "Prototype Item", typeof(RectTransform), typeof(Image), typeof(DynamicScrollViewItemExample) ).GetComponent<RectTransform>();
            prototypeItemRect.SetParent( contentRect, false );
            if( this._direction == Direction.Horizontal ) prototypeItemRect.setSizeFromLeft(0.23f); else prototypeItemRect.setSizeFromTop(0.23f);
            var prototypeItem = prototypeItemRect.GetComponent<DynamicScrollViewItemExample>();
            var prototypeItemBg = prototypeItemRect.GetComponent<Image>();
            prototypeItemBg.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            prototypeItemBg.type = Image.Type.Sliced;
            prototypeItem.background = prototypeItemBg;
            this.itemPrototype = prototypeItemRect;

            // [ ScrollRect / Viewport / Content / PrototypeItem / Title ]

            var prototypeTitleRect = new GameObject( "Title", typeof(RectTransform), typeof(Text) ).GetComponent<RectTransform>();
            prototypeTitleRect.SetParent( prototypeItemRect, false );
            prototypeTitleRect.setFullSize();
            var prototypeTitleSize = prototypeTitleRect.getSize();
            prototypeTitleRect.setSize( prototypeTitleSize-prototypeTitleSize*0.1f );
            var title = prototypeTitleRect.GetComponent<Text>();
            title.fontSize              = 16;
            title.alignment             = TextAnchor.MiddleCenter;
            title.horizontalOverflow    = HorizontalWrapMode.Wrap;
            title.verticalOverflow      = VerticalWrapMode.Truncate;
            title.color                 = Color.black;
            title.text                  = "Name000";
            title.resizeTextForBestFit  = true;
            title.resizeTextMinSize     = 9;
            title.resizeTextMaxSize     = 40;
            prototypeItem.title = title;
        }
        #endif

        #endregion [ Editor ]

    }
}
