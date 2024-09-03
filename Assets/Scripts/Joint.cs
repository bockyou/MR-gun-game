using UnityEngine;
using System.Collections.Generic;

public class Joint : MonoBehaviour
{
    [SerializeField] private OVRHand rightHand; // Meta XR SDK의 OVRHand 클래스
    [SerializeField] private GameObject jointPrefab; // 작은 구체 또는 포인트 표시 오브젝트 프리팹
    [SerializeField] private Material lineMaterial; // LineRenderer에 사용할 재질

    private List<GameObject> rightHandJoints = new List<GameObject>();
    private List<LineRenderer> jointLines = new List<LineRenderer>();
    private Dictionary<OVRSkeleton.BoneId, int> boneIndexMap = new Dictionary<OVRSkeleton.BoneId, int>();

    void Start()
    {
        if (rightHand != null)
        {
            InitializeHandJoints(rightHand, rightHandJoints);
        }
    }

    void InitializeHandJoints(OVRHand hand, List<GameObject> handJoints)
    {
        OVRSkeleton skeleton = hand.GetComponent<OVRSkeleton>();

        // Clear any existing joints and lines
        foreach (var joint in handJoints)
        {
            Destroy(joint);
        }
        handJoints.Clear();

        foreach (var line in jointLines)
        {
            Destroy(line.gameObject);
        }
        jointLines.Clear();

        // Create new joints and map bone indices
        for (int i = 0; i < skeleton.Bones.Count; i++)
        {
            var bone = skeleton.Bones[i];
            if (bone.Transform != null)
            {
                GameObject joint = Instantiate(jointPrefab, bone.Transform.position, Quaternion.identity, this.transform);
                handJoints.Add(joint);
                boneIndexMap[bone.Id] = i;

                // Create lines between joints of the same finger
                if (IsFingerBone(bone.Id) && i > 0 && IsFingerBone(skeleton.Bones[i - 1].Id))
                {
                    LineRenderer line = new GameObject("JointLine").AddComponent<LineRenderer>();
                    line.transform.parent = this.transform;
                    line.material = lineMaterial;
                    line.startWidth = 0.01f;
                    line.endWidth = 0.01f;
                    line.positionCount = 2;
                    jointLines.Add(line);
                }
            }
        }
    }

    void Update()
    {
        if (rightHand != null)
        {
            UpdateHandJoints(rightHand, rightHandJoints);
            UpdateHandLines(rightHandJoints, jointLines);
        }
    }

    void UpdateHandJoints(OVRHand hand, List<GameObject> handJoints)
    {
        OVRSkeleton skeleton = hand.GetComponent<OVRSkeleton>();

        // Ensure handJoints list has enough elements
        if (handJoints.Count != skeleton.Bones.Count)
        {
            Debug.LogWarning("Mismatch between number of joints and number of bones. Reinitializing joints.");
            InitializeHandJoints(hand, handJoints);
            return;
        }

        for (int i = 0; i < skeleton.Bones.Count; i++)
        {
            if (i < handJoints.Count)
            {
                Transform boneTransform = skeleton.Bones[i].Transform;
                if (boneTransform != null)
                {
                    handJoints[i].transform.position = boneTransform.position;
                }
            }
        }
    }

    void UpdateHandLines(List<GameObject> handJoints, List<LineRenderer> jointLines)
    {
        int lineIndex = 0;
        for (int i = 1; i < handJoints.Count; i++)
        {
            var currentBoneId = rightHand.GetComponent<OVRSkeleton>().Bones[i].Id;
            var previousBoneId = rightHand.GetComponent<OVRSkeleton>().Bones[i - 1].Id;

            if (IsFingerBone(currentBoneId) && IsFingerBone(previousBoneId) && AreBonesInSameFinger(currentBoneId, previousBoneId))
            {
                LineRenderer line = jointLines[lineIndex++];
                line.SetPosition(0, handJoints[boneIndexMap[previousBoneId]].transform.position);
                line.SetPosition(1, handJoints[boneIndexMap[currentBoneId]].transform.position);
            }
        }
    }

    bool IsFingerBone(OVRSkeleton.BoneId boneId)
    {
        return boneId >= OVRSkeleton.BoneId.Hand_Thumb0 && boneId <= OVRSkeleton.BoneId.Hand_Pinky3;
    }

    bool AreBonesInSameFinger(OVRSkeleton.BoneId boneId1, OVRSkeleton.BoneId boneId2)
    {
        if (boneId1 == OVRSkeleton.BoneId.Hand_ThumbTip || boneId2 == OVRSkeleton.BoneId.Hand_ThumbTip)
            return false;

        if (boneId1 == OVRSkeleton.BoneId.Hand_IndexTip || boneId2 == OVRSkeleton.BoneId.Hand_IndexTip)
            return false;

        if (boneId1 == OVRSkeleton.BoneId.Hand_MiddleTip || boneId2 == OVRSkeleton.BoneId.Hand_MiddleTip)
            return false;

        if (boneId1 == OVRSkeleton.BoneId.Hand_RingTip || boneId2 == OVRSkeleton.BoneId.Hand_RingTip)
            return false;

        if (boneId1 == OVRSkeleton.BoneId.Hand_PinkyTip || boneId2 == OVRSkeleton.BoneId.Hand_PinkyTip)
            return false;

        return ((int)boneId1 / 4) == ((int)boneId2 / 4);
    }
}
