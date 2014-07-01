/// Copyright (C) 2012-2014 Soomla Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///      http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.

using UnityEngine;
using System;

namespace Soomla {
	
	public class RewardStorageIOS : RewardStorage {

#if UNITY_IOS && !UNITY_EDITOR

		[DllImport ("__Internal")]
		private static extern void rewardStorage_SetRewardStatus(IntPtr rewardJson, [MarshalAs(UnmanagedType.Bool)] bool give);
		[DllImport ("__Internal")]
		[return:MarshalAs(UnmanagedType.I1)]
		private static extern bool rewardStorage_IsRewardGiven(IntPtr rewardJson);
		[DllImport ("__Internal")]
		private static extern int rewardStorage_GetLastSeqIdxGiven(IntPtr rewardJson);
		[DllImport ("__Internal")]
		private static extern void rewardStorage_SetLastSeqIdxGiven(IntPtr rewardJson, int idx);

		/// <summary>
		/// Set the reward given status
		/// </summary>
		/// <param name="reward">to set status for</param>
		/// <param name="give">true to give, false to take</param>
		/// <returns></returns>
		override protected void _setRewardStatus(Reward reward, bool give) {
			string rewardJson = reward.toJSONString();
			rewardStorage_SetRewardStatus(rewardJson, give);
			
//			int err = rewardStorage_SetRewardStatus(rewardJson, give);
//			IOS_ErrorCodes.CheckAndThrowException(err);
		}

		/// <summary>
		/// Check the reward given status
		/// </summary>
		/// <param name="reward">to query</param>
		/// <returns>true if reward was given</returns>
		override protected bool _isRewardGiven(Reward reward) {
			string rewardJson = reward.toJSONString();
			return rewardStorage_IsRewardGiven(rewardJson);
		}
		
		/// <summary>
		/// Get last id of given reward from a <c>SequenceReward</c>
		/// </summary>
		/// <param name="reward">to query</param>
		/// <returns>true if reward was given</returns>
		override protected bool _getLastSeqIdxGiven(SequencneReward seqReward) {
			string rewardJson = reward.toJSONString();
			return rewardStorage_GetLastSeqIdxGiven(rewardJson);
		}
		
		/// <summary>
		/// Set last id of given reward from a <c>SequenceReward</c>
		/// </summary>
		/// <param name="reward">to set last id for</param>
		/// <param name="reward">the last id to to mark as given</param>
		/// <returns>true if reward was given</returns>
		override protected bool _setLastSeqIdxGiven(SequencneReward seqReward, int idx) {
			string rewardJson = reward.toJSONString();
			return rewardStorage_SetLastSeqIdxGiven(rewardJson, idx);
		}		
#endif
	}
}
