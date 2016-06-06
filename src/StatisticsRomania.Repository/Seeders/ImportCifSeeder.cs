﻿using System.Collections.Generic;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders
{
    internal class ImportCifSeeder : BaseSeeder
    {
        internal static List<ImportCif> GetData()
        {
            var rawData = new List<string>()
                              {
                                  // 2014, 2015
                                  "2014 10 Alba 48212 47661 45107 35758 48916 58239 46052 54023 52484 53825 44984 51495 53099 50993 58162",
                                  "2014 10 Arad 230502 213119 177039 193974 209168 246185 226557 223233 239478 243639 198076 247669 253032 237445 186185",
                                  "2014 10 Arges 365805 323459 272226 314047 323744 354820 332661 322119 348530 356185 216594 378952 366901 334302 301696",
                                  "2014 10 Bacau 42854 39482 29312 32916 49463 55251 49980 48582 57307 54469 43033 55893 55412 51793 43054",
                                  "2014 10 Bihor 190601 179009 135398 153467 157936 167516 161205 156043 159487 164475 138536 177700 184295 175206 151829",
                                  "2014 10 BistritaNasaud 48727 47728 38951 43257 42021 48548 42463 49818 57170 55184 43163 61716 63671 58160 51210",
                                  "2014 10 Botosani 18923 23098 19131 18274 17872 22718 23049 23448 23076 23848 12935 22269 22141 23000 20059",
                                  "2014 10 Brasov 229691 217409 195364 181512 211836 228814 207513 201402 223538 238106 202282 236818 239401 221042 200411",
                                  "2014 10 Braila 18015 15449 16678 15037 10835 13171 14171 14516 20284 14929 11072 13836 15607 13361 12615",
                                  "2014 10 Buzau 41460 43900 29129 36100 27639 51764 41054 29219 49327 59158 45889 38131 51415 33519 75750",
                                  "2014 10 CarasSeverin 18652 16607 12676 16378 16521 19041 18064 16441 19210 20019 17388 17821 20007 19892 18983",
                                  "2014 10 Calarasi 22067 21875 15915 16382 22622 28609 30201 22569 29099 33164 20525 27331 17699 18894 15055",
                                  "2014 10 Cluj 184009 172960 155099 124955 139789 171016 167207 168602 171560 189104 160034 191642 202734 184686 173936",
                                  "2014 10 Constanta 308698 236836 237810 161989 190021 247099 248040 253420 211112 346237 240896 238791 166339 107485 188399",
                                  "2014 10 Covasna 27279 26166 21076 19989 22083 25013 22504 22069 23737 22016 21133 25675 25716 24044 21748",
                                  "2014 10 Dambovita 41979 36813 26673 31572 32879 41923 41457 42329 50905 51085 48271 43661 49802 44336 42432",
                                  "2014 10 Dolj 81666 66199 62881 73673 88189 85899 68741 75009 84669 86328 57271 93501 82772 75385 67655",
                                  "2014 10 Galati 78819 87217 66092 69169 69201 69137 66716 81859 80201 87993 90794 76560 82983 81288 82155",
                                  "2014 10 Giurgiu 12606 10010 7508 5926 7661 10769 7531 9062 15374 10026 7065 10009 9438 10406 9965",
                                  "2014 10 Gorj 6103 3602 4616 4536 3824 4061 4576 3636 3990 3870 4207 4226 3411 4894 2872",
                                  "2014 10 Harghita 37859 28059 23854 22825 24228 33727 29842 30418 35473 33607 28792 37205 34668 35333 27430",
                                  "2014 10 Hunedoara 48368 47998 46146 45925 51741 62097 50104 43677 46024 49175 35136 50868 51860 50711 46036",
                                  "2014 10 Ialomita 12181 10048 7619 7523 9537 11714 11979 11585 16247 14839 15718 14826 12683 18213 20243",
                                  "2014 10 Iasi 55714 55075 43183 42600 50970 64927 58432 57076 68986 65508 56766 68578 71161 65113 67406",
                                  "2014 10 Ilfov 389247 373346 350331 302308 348048 414896 384950 375771 423107 432340 374812 441123 450367 447786 428074",
                                  "2014 10 Maramures 71409 66193 52631 52558 59172 66274 63168 61178 71784 72415 58275 77134 75459 81627 64387",
                                  "2014 10 Mehedinti 7544 6375 4868 6570 7389 7992 7213 8419 9228 8534 5024 9485 7866 8446 4718",
                                  "2014 10 Mures 89008 89482 95169 81675 78673 102642 85213 81286 97161 110718 77233 96273 114651 94726 91950",
                                  "2014 10 Neamt 26781 24098 24437 19530 26396 37128 30341 28016 38564 30855 27596 27672 32516 30054 24861",
                                  "2014 10 Olt 62789 55542 38984 47747 48722 63553 51812 49191 58944 59439 47701 52811 48136 50719 41314",
                                  "2014 10 Prahova 261010 247410 247664 171846 235883 270786 164968 229257 242861 240376 230968 245383 252705 237126 216417",
                                  "2014 10 SatuMare 76828 72828 60678 60287 66322 76254 73766 68066 74160 82293 80937 87223 87397 78948 65764",
                                  "2014 10 Salaj 27934 22869 22368 30361 24426 25495 21484 21842 23430 28999 16824 23050 26350 21836 17518",
                                  "2014 10 Sibiu 179068 162919 137582 148423 152920 171377 158236 148387 162867 167563 136281 163879 192477 168466 141962",
                                  "2014 10 Suceava 37114 31266 30969 31530 36057 41700 40382 39149 45563 48198 40333 47538 46334 42929 40563",
                                  "2014 10 Teleorman 11394 11505 9800 10153 9813 11787 13182 26030 17653 16450 11761 15051 13154 10816 10613",
                                  "2014 10 Timis 386957 354973 304349 267731 356230 395190 368990 362601 394173 413288 332114 403307 420650 388238 352534",
                                  "2014 10 Tulcea 21883 23123 19733 21408 24645 33046 24988 34762 21179 24958 15038 21450 17189 19389 16188",
                                  "2014 10 Vaslui 14326 10589 9592 9311 10458 10926 13244 14201 15074 13242 7601 11615 11494 10891 9750",
                                  "2014 10 Valcea 23490 20412 16957 15247 21572 25514 23051 19851 23482 25248 21092 25228 22519 22121 23560",
                                  "2014 10 Vrancea 20157 15635 14718 15473 17634 20556 23160 20208 25024 20479 13983 22138 22977 18235 16131",
                                  "2014 10 Bucuresti 1586809 1517911 1400884 1373561 1406178 1687312 1503886 1461430 1600810 1586486 1490970 1679278 1868318 1827888 1631570",
                                  // 2016
                                  "2016 1 Alba 51880",
                                  "2016 1 Arad 211439",
                                  "2016 1 Arges 319248",
                                  "2016 1 Bacau 39891",
                                  "2016 1 Bihor 142309",
                                  "2016 1 BistritaNasaud 48442",
                                  "2016 1 Botosani 15154",
                                  "2016 1 Brasov 191559",
                                  "2016 1 Braila 9209",
                                  "2016 1 Buzau 28673",
                                  "2016 1 CarasSeverin 16636",
                                  "2016 1 Calarasi 15350",
                                  "2016 1 Cluj 136288",
                                  "2016 1 Constanta 109113",
                                  "2016 1 Covasna 17730",
                                  "2016 1 Dambovita 33088",
                                  "2016 1 Dolj 71620",
                                  "2016 1 Galati 48836",
                                  "2016 1 Giurgiu 6813",
                                  "2016 1 Gorj 2826",
                                  "2016 1 Harghita 24119",
                                  "2016 1 Hunedoara 46738",
                                  "2016 1 Ialomita 9486",
                                  "2016 1 Iasi 48273",
                                  "2016 1 Ilfov 339796",
                                  "2016 1 Maramures 60903",
                                  "2016 1 Mehedinti 6092",
                                  "2016 1 Mures 84624",
                                  "2016 1 Neamt 25451",
                                  "2016 1 Olt 50122",
                                  "2016 1 Prahova 194217",
                                  "2016 1 SatuMare 66234",
                                  "2016 1 Salaj 19688",
                                  "2016 1 Sibiu 150102",
                                  "2016 1 Suceava 39293",
                                  "2016 1 Teleorman 10626",
                                  "2016 1 Timis 364473",
                                  "2016 1 Tulcea 11973",
                                  "2016 1 Vaslui 9985",
                                  "2016 1 Valcea 16240",
                                  "2016 1 Vrancea 12822",
                                  "2016 1 Bucuresti 1367433",
                              };

            var items = GetItems<ImportCif>(rawData);

            return items;
        }
    }
}
