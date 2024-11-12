using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueData : MonoBehaviour
{
    public class DialogueTalk
    {
        public int npcId;
        public int questId;
        public string[] dialogueLines;

        public int cutScene;

        public DialogueTalk(int _talkIndex, int _questId, string[] _dialogueLines)
        {
            npcId = _talkIndex;
            questId = _questId;
            dialogueLines = _dialogueLines;
        }

        public DialogueTalk(int _cutScene, string[] _dialogueLines)
        {
            cutScene = _cutScene;
            dialogueLines = _dialogueLines;
        }
    }

    private List<DialogueTalk> dialogueTalk;
    private List<DialogueTalk> dialogueCutSceneTalk;

    private void Awake()
    {
        dialogueTalkManage();
    }

    /// <summary>
    /// ´ÙÀÌ¾ó·Î±× ´ëÈ­¸¦ °ü¸®ÇÏ´Â ÇÔ¼ö
    /// </summary>
    private void dialogueTalkManage()
    {
        dialogueTalk = new List<DialogueTalk>
        {
            setDialogueTalk(1000, 0,
            "¾ðÁ¦µçÁö ¶Ç ³îÀÚ!",
            "³Î ±â´Ù¸±°Ô!"),
            setDialogueTalk(1000, 100,
            "ÀÏ¾î³µ±¸³ª!",
            "±â´Ù¸®°í ÀÖ¾ú¾î.",
            "°°ÀÌ ÆÛÁñ ¸ÂÃß±â ³îÀÌ ÇÒ·¡?"),
            setDialogueTalk(1000, 101,
            "Àç¹Õ¾úÁö?",
            "¿ì¸® °£½Ä ¸Ô°í ¶Ç ÇÏÀÚ."),
            setDialogueTalk(2000, 0,
            "¿À´ÃÀº ¿©±â±îÁö",
            "´ÙÀ½¿¡ ³îÀÚ±¸~"),
            setDialogueTalk(2000, 110,
            "½É½ÉÇÏ¸é ³ª¶û ³î·¡?",
            "µµÅä¸®°¡ ¾îµð¿¡ ÀÖ´Â Áö ¸ÂÇôºÁ!",
            "½±Áö ¾ÊÀ» °Å ¶ó±¸~"),
            setDialogueTalk(2000, 111,
            "ÀßÇÏ´Âµ¥?",
            "´ÙÀ½¿¡´Â ¸ÂÃß±â ¾î·Á¿ï °É~"),
            setDialogueTalk(2000, 115,
            "´ÙÀ½¿¡ ´õ ³ë·ÂÇØºÁ",
            "±×·³ Àß°¡~"),
            setDialogueTalk(3000, 0,
            "Ç°¿¡ ²À ¾È°í ´Ù´Ò °Å¾ß... Ã£¾ÆÁà¼­ °í¸¶¿ö..!",
            "ÀÌÁ¦ ´õ ÀÌ»ó ÀÒ¾î¹ö¸®Áö ¾ÊÀ»°Ô"),
            setDialogueTalk(3000, 200,
            "³» »ý¼± ÀÎÇüÀÌ »ç¶óÁ³¾î.....",
            "µµ¿ÍÁà...."),
            setDialogueTalk(3000, 201,
            "³» »ý¼± ÀÎÇü..!",
            "°í¸¶¿ö!!!"),
            setDialogueTalk(4000, 0,
            "¾ÕÀ¸·Î´Â ÀÒ¾î¹ö¸®Áö ¾ÊÀ»°Ô °í¸¶¿ö",
            "°í¸¶¿ö"),
            setDialogueTalk(4000, 210,
            "¡¦¡¦.",
            "³» ´ç±Ù Àå³­°¨À» ÁÖ¹æ¿¡¼­ ÀÒ¾î¹ö¸° °Í °°Àºµ¥", 
            "Ã£Áö¸¦ ¸øÇØ¼­ µ¹¾Æ¿Ô¾î.."),
            setDialogueTalk(4000, 211,
            "Ã£¾ÆÁà¼­ °í¸¶¿ö",
            "´ÙÀ½¿¡ µµ¿òÀÌ ÇÊ¿äÇÏ¸é µµ¿ÍÁÙ°Ô, Àß°¡"),

            setDialogueTalk(1000, 150,
            "ÀÌ°Å¶û~ Àú°Å¶û~",
            "¸ðµÎ ±×·Á, °¡µæ~ °¡µæ~"),
            setDialogueTalk(2000, 160,
            "¡¦.µµ¡¦Åä¸®¡¦",
            "µµÅä¸®..¤·¤©..¤·¤¤"),
            setDialogueTalk(3000, 250,
            "³» ÀÎÇü ³»³ö..",
            "³»³õÀ¸¶ó°í.. ³»³ö!!!!!"),
            setDialogueTalk(4000, 260,
            "È÷È÷ÆRÆRÈ÷È÷È÷¤Ó",
            "È÷ÆRÈ÷È÷È÷È÷È÷È÷ÆR¤Ó"),  
        };

        dialogueCutSceneTalk = new List<DialogueTalk>
        {
            setDialogueCutSceneTalk(10,
            "ÀÚ~ ¿©·¯ºÐ, °£½Ä ½Ã°£ÀÌ¿¡¿ä",
            "´ÞÄÞÇÑ »çÅÁÀ» ÁÙ°Ô¿ä~")
        };
    }

    private DialogueTalk setDialogueTalk(int _npcId, int questId, params string[] dialogues)
    {
        DialogueTalk newDialogue = new DialogueTalk(_npcId, questId, dialogues);
        return newDialogue;
    }

    private DialogueTalk setDialogueCutSceneTalk(int _cutScene, params string[] dialogues)
    {
        DialogueTalk newDialogue = new DialogueTalk(_cutScene, dialogues);
        return newDialogue;
    }

    /// <summary>
    /// ´ÙÀÌ¾ó·Î±× ´ëÈ­¸¦ °¡Á®¿À±â À§ÇÑ ÇÔ¼ö
    /// </summary>
    /// <param name="_npcId"></param>
    /// <param name="_questId"></param>
    /// <returns></returns>
    public List<string> GetDialogue(int _npcId, int _questId)
    {
        for (int iNum = 0; iNum < dialogueTalk.Count; iNum++)
        {
            if (dialogueTalk[iNum].npcId == _npcId
                && dialogueTalk[iNum].questId == _questId)
            {
                List<string> dialogueList = new List<string>(dialogueTalk[iNum].dialogueLines);
                return dialogueList;
            }
        }

        return null;
    }

    public List<string> GetCutSceneDialogue(int _cutScene)
    {
        for (int iNum = 0; iNum < dialogueCutSceneTalk.Count; iNum++)
        {
            if (dialogueCutSceneTalk[iNum].cutScene == _cutScene)
            {
                List<string> dialogueList = new List<string>(dialogueCutSceneTalk[iNum].dialogueLines);
                return dialogueList;
            }
        }

        return null;
    }
}
