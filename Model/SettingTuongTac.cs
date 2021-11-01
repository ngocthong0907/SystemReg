using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class SettingTuongTac
    {
        public string nameConfig { set; get; }
        public int delaymin { set; get; }
        public int delaymax { set; get; }
        public int action { set; get; }
        public bool readnoti { set; get; }
        //profile
        public bool luotnewfeed { set; get; }
        public bool luotStory { set; get; }
        public bool likenewfeed { set; get; }

        public bool chklikestory { set; get; }
        public bool chkcommentstory { set; get; }
        public int numlikenewfeedmin { set; get; }
        public int numlikenewfeedmax { set; get; }
        public int numlikestorymin { set; get; }
        public int numlikestorymax { set; get; }
        public int numcommentstorymin { set; get; }
        public int numcommentstorymax { set; get; }
        public bool commentnewfeed { set; get; }
        public int numcommentnewfeedmin { set; get; }
        public int numcommentnewfeedmax { set; get; }
        public bool sharevideo { set; get; }
        public int numsharevideo { set; get; }
        public string message { set; get; }

        public string pathImagecomment { set; get; }

        public string pathImagecomment_gr { set; get; }
        public bool chkcommentImage { set; get; }
        public bool chkcommentImage_gr { set; get; }
        public bool chkdelcommentImage { set; get; }
        public bool chkdelcommentImage_gr { set; get; }

        public bool chkcontentImage { set; get; }
        public bool chkcontentImage_gr { set; get; }
        public int numcommentImage { set; get; }
        public int numcommentImage_max { set; get; }

        public int numcommentImage_gr { set; get; }
        public int numcommentImage_max_gr { set; get; }

        public bool sharepost { set; get; }
        public int numsharepostmin { set; get; }
        public int numsharepostmax { set; get; }
        //friend
        public bool addfriend { set; get; }
        public int numaddfriendmin { set; get; }
        public int numaddfriendmax { set; get; }
        public bool addfriendNewfeed { set; get; }
        public int numaddfriendNewfeedmin { set; get; }
        public int numaddfriendNewfeedmax { set; get; }
        public bool acceptfriend { set; get; }

        public bool getBirthday { set; get; }

        public int numacceptfriendmin { set; get; }
        public int numacceptfriendmax { set; get; }

        public bool cancelrequest { set; get; }
        public int numcacelrequestmin { set; get; }
        public int numcacelrequestmax { set; get; }
        public int numslidemin { set; get; }
        public int numslidemax { set; get; }
        public int numslidestorymin { set; get; }
        public int numslidestorymax { set; get; }
        //fanpage
      

        public string strkeywordfanpage { set; get; }
        public bool likefanpage { set; get; }
        public int numlikefanpagemin { set; get; }
        public int numlikefanpagemax { set; get; }
        public bool likepostfanpage { set; get; }
        public int numlikepostfanpagemin { set; get; }
        public int numlikepostfanpagemax { set; get; }
        public bool commentpostfanpage { set; get; }
        public int numcommentpostfanpagemin { set; get; }
        public int numcommentpostfanpagemax { set; get; }
        public string strseachInfo { set; get; }
        //group
        public string strkeywordseach { set; get; }
        public bool joingroupkeyword { set; get; }
        public int numjoingroupkeywordmin { set; get; }
        public int numjoingroupkeywordmax { set; get; }
        public bool likepostgroup { set; get; }
        public int numlikepostgroupmin { set; get; }
        public int numlikepostgroupmax { set; get; }
        public bool commentpostgroup { set; get; }
        public int numcommentpostgroupmin { set; get; }
        public int numcommentpostgroupmax { set; get; }
        //seeding
        public bool seeding { set; get; }
        public string seedinglink { set; get; }
        public bool shareSeeding { set; get; }
        public bool likeSeeding { set; get; }
        public bool chklike { set; get; }
        public bool chklove { set; get; }
        public bool chkhaha { set; get; }
        public bool chkwow { set; get; }
        public bool chksad { set; get; }
        public bool chkangry { set; get; }

        //share2group
        public bool chkShareGroup { set; get; }
        public string strlink { set; get; }
        public string strcontentshare { set; get; }
        public int numGroup { set; get; }

        //join group by uid
        public bool chkUID { set; get; }
        public bool chkAutoAnwser { set; get; }
        public string strPath { set; get; }
        public int numGroupUIDMin { set; get; }
        public int numGroupUIDMax { set; get; }

        //loop tuong tac
        public bool chkLoop_tuongtac { set; get; }
        public int numLoop_tuongtac{ set; get; }
        public int timestart { set; get; }
        public int timestop { set; get; }

        //friend UID
        public bool chkAddFriendUID { set; get; }

        public bool chkaddfriendlink { set; get; }

        public string txtfilelink { set; get; }

        public bool chkCanceldFriendUID { set; get; }

        public bool chkCanceldFriendRandom { set; get; }

        public int numcancelfrienduidmin { set; get; }
        public int numcancelfrienduidmax { set; get; }
        public string pathCancelFriendUID { set; get; }

        public int numaddfrienduidmin { set; get; }
        public int numaddfrienduidmax { set; get; }

        public int numaddfriendlinkmin { set; get; }
        public int numaddfriendlinkmax { set; get; }
       
        public int numthread { set; get; }
        public int numdelayld { set; get; }

        public bool runRandom { set; get; }

        public bool searchInfo { set; get; }

        public bool likeCommentGroupId { set; get; }

        public string pathGroupIdLikecomment { set; get; }

        public bool chkMessenger { set; get; }

        public int numMessenger { set; get; }

        public string pathReaction { set; get; }
        public string strPathAnswer { set; get; }
        public string strPathCommentGroup { set; get; }

        public bool has_newfeed_watch { set; get; }
        public int timenewfeedwatchmin { set; get; }
        public int timenewfeedwatchmax { set; get; }

        public int timeviewvideomin { set; get; }
        public int timeviewvideomax { set; get; }
        public bool has_like_newfeed_watch { set; get; }
        public int likenewfeedwatchmin { set; get; }
        public int likenewfeedwatchmax { set; get; }
        public bool has_newfeed_marketplace { set; get; }
        public int timenewfeedmarketplacemin { set; get; }
        public int timenewfeedmarketplacemax { set; get; }

        public int numscrollgroupmin { set; get; }
        public int numscrollgroupmax { set; get; }
        public bool chkscrollgroup { set; get; }
      

        public bool has_like_newfeed_group { set; get; }
        public int num_like_newfeed_groupmin { set; get; }
        public int num_like_newfeed_groupmax { set; get; }
        //remove group
        public bool removegroup { set; get; }
        public int numremovegroup_min { set; get; }
        public int numremovegroup_max { set; get; }

        public bool remove_choduyet { set; get; }
        public bool remove_thanhvien { set; get; }
        public int remove_num_thanhvien { set; get; }
        public bool remove_post { set; get; }
        public int remove_num_post { set; get; }

        //cho duyet
        public bool joingroupnoapprove { set; get; }

        public int numjoingroupsuggestmin { set; get; }
        public int numjoingroupsuggestmax { set; get; }
        public bool chkjoingruoupsuggest { set; get; }

        //dang bai group
        public bool postgroup { set; get; }
        public int postgroup_minday { set; get; }
        public int postgroup_maxday { set; get; }
        public bool postgrouprandompost { set; get; }
        public bool postprofile { set; get; }
        public int postprofile_minday { set; get; }
        public int postprofile_maxday { set; get; }
        public bool postprofile_randompost { set; get; }

        public bool runrandomtuongtac { set; get; }
        public int numrandomtuongtac { set; get; }
        public bool khangspam { set; get; }
        public int nummaxtuongtac { set; get; }

        public bool has_addfriendgroup { set; get; }
        public int numaddfriendgroupmin { set; get; }
        public int numaddfriendgroupmax{ set; get; }

        public bool likeavatar { set; get; }
        public bool commentnewfeedgroup { set; get; }
        public int numcommentnewfeedgroupmin { set; get; }
        public int numcommentnewfeedgroupmax { set; get; }
    }
}
