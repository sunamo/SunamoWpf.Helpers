namespace SunamoWpf.Helpers.ControlsWithGet.Base;

public  abstract partial class ImageHelperBase<ImageSource, ImageControl>
    {
        public abstract ImageControl MsAppxI(string appPic2);
        public abstract ImageControl MsAppx(string relPath);
        public abstract ImageControl MsAppx(bool disabled, AppPics appPic);
    }