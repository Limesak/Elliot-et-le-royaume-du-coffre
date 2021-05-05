// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/Movements.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Movements : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Movements()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Movements"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""1eb6c479-92cb-4498-b231-6ebc825e4db3"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""d58f5d34-cd6e-4c1d-828c-b3e7378eea02"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""35e8252f-bd81-4322-9a55-5c8501139d7b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""afdcb001-a8b7-4aab-8e05-8e451821abe8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lock"",
                    ""type"": ""Button"",
                    ""id"": ""6fdc55ea-cc06-4a9a-8722-79c71700d58c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""94c117e2-6535-47ef-9b86-4d72bc7370c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackUse"",
                    ""type"": ""Button"",
                    ""id"": ""58d7379f-c9d7-447f-a44b-696df02c0c70"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""99fd7851-0765-4dc9-85b3-159d65d6269d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ConsumeCandy"",
                    ""type"": ""Button"",
                    ""id"": ""eec8bea4-1fd1-4562-8c96-a5cd9cda36bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""636781ce-7af1-4179-a97a-347b296ab9b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchToolBefore"",
                    ""type"": ""Button"",
                    ""id"": ""490e20b6-18dd-42de-acd8-3632271d68ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchToolAfter"",
                    ""type"": ""Button"",
                    ""id"": ""ae23e239-5dd2-4886-8109-7ee423d1fd28"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fb9d05cd-2e9f-4494-9e13-bf3bead93cc5"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fce76eb1-997e-4892-8583-982b4d0e67eb"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6085bdf1-622e-4664-a44a-3bb49d730944"",
                    ""path"": ""<DualShockGamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1cccd6a1-c7f5-4652-b362-41e971f0477f"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5a44429-a98c-47ec-bb4b-89a4d3e80e66"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae8cbe23-50dc-4fbd-ab9b-b5d97e901188"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d42da0c6-0a94-4b16-8038-c114368407fb"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8fc4c8c-96cd-4946-a737-64b8c9921940"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ef0d081-61a8-402b-806e-4fd445eb08de"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43462625-f339-43d4-bb75-890e73861176"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c54a6066-78ce-4fba-bfa2-0e66785a5793"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2983e5df-d4bb-4163-991b-a738bfb32eef"",
                    ""path"": ""<DualShockGamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1e4706d-b534-4547-bb83-2be365e0b3f7"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""adcb3ad0-75ca-4ca8-9526-9dfa326f5bb0"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0f4e4b9-6c89-46a5-ad56-a4dd0cbea52e"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b999ed2f-38d4-4164-ae96-b00f65ff6071"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackUse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5335fb92-9cb1-405e-8659-6eefb794e36f"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackUse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f46ebca-36cd-42a3-bb04-854b851a4d4b"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackUse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7f62c4f-e81e-4d07-a04e-d6d50e7341f0"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b21d6094-c6d2-4aa7-be66-c2ae230b8d51"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b02b574-dc41-43be-bd9e-ba588415f6e0"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16172a10-c58f-4dfb-86c2-46cf8243c794"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConsumeCandy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27225c58-52c7-4345-9d1c-51b667a152be"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConsumeCandy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f990e09-b85c-4f6d-8038-65f27116abb8"",
                    ""path"": ""<DualShockGamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConsumeCandy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12130f42-63d8-4b5c-80b3-d7ae975f5294"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdfeaddd-1c5c-40ae-af9d-93a03636f256"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""709df7b2-e444-482a-998d-187a4355f69c"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1fae1152-5f4c-4e35-af62-0f15734150e2"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToolBefore"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a853fd94-6c70-43a2-9242-8be87277f037"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToolBefore"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""142402cd-4b16-424b-aaf0-2420a6088fd6"",
                    ""path"": ""<DualShockGamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToolBefore"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84c10ce7-a5c7-4f0c-843f-1c3540c182ca"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToolAfter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""538c8f0d-b52f-438a-b9ea-4ffdaa80074d"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToolAfter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8de54672-455b-42e2-874d-2925bbc8d0e3"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToolAfter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player1"",
            ""id"": ""125665d3-017c-4d41-a50b-3166329b31ba"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""bbb64a0a-a53e-44cf-8600-d33c1f426ea9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""a98a8176-4cc3-42b9-a26a-da32e2b48271"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""7adf7d7f-d62e-4479-b43b-ecb6ec944737"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lock"",
                    ""type"": ""Button"",
                    ""id"": ""18cc9901-992e-4324-9e95-6b5dff8663c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""3908e90a-aa26-4619-9327-4de1f46adc3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackUse"",
                    ""type"": ""Button"",
                    ""id"": ""ee3825ef-6bf5-4ca3-91de-9a19ab580314"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""e1142aa7-1ecb-493a-a2af-2175450925c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""d65cfdcd-e7fb-4621-b0b4-bfe107bb6970"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ConsumeCandy"",
                    ""type"": ""Button"",
                    ""id"": ""32cb872b-cc7a-48f4-a0b4-1fd498bc5531"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchToolBefore"",
                    ""type"": ""Button"",
                    ""id"": ""43f8b457-0aed-4729-bee7-8539e59899e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchToolAfter"",
                    ""type"": ""Button"",
                    ""id"": ""348f2c7a-2ea1-4e8f-85d6-92804a84dcdf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a47105dc-5958-4dc1-b124-b82241a5af41"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eed0f8eb-c32d-470a-ba97-44ed142d4ca8"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd81177c-18a7-40e0-82d1-c8de8c4a6215"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40c10a75-d663-4b50-978a-57aa3d58da0c"",
                    ""path"": ""<XInputController>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67719c18-103a-49c7-9846-6c00daa8627b"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""131e275a-5b6b-4467-a173-ce9af3f750c7"",
                    ""path"": ""<XInputController>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47da3a4c-85be-48a6-8967-e9e91ab21dfc"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2826544c-436c-46ea-a97e-2f2ab8a06832"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3284197b-4a7d-43f6-ac95-1d1dd1557b21"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b36a735a-9e35-44f4-845c-9acccdb4cff5"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b594fd0b-2350-4a89-865d-6b09c3473e64"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackUse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94d26cf0-b415-40a6-8305-b87d56dc7204"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackUse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46a1cc15-05dd-42d1-9de7-45f8849de412"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f45390ec-62d5-4f86-8e3e-3a70a319a817"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3678898-c61b-4919-9910-98eba87df666"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConsumeCandy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b74d0a8-b5a4-42ac-9e71-3f5178c814ef"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConsumeCandy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37d60b10-cc1c-44a9-a764-952b3c552331"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f6c1d1c-d09f-4eeb-9512-40e58f87ad76"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""677f2d04-1b6b-4392-90d4-b115d95f3150"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToolAfter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24dc363a-be89-4f72-9ccb-db832bc93362"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToolAfter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4fc875b6-fb6d-4529-b281-f1ffd3516d38"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToolBefore"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8a66f78-1c0b-4c9d-8995-58d479b5762c"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToolBefore"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Lock = m_Player.FindAction("Lock", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_AttackUse = m_Player.FindAction("AttackUse", throwIfNotFound: true);
        m_Player_Menu = m_Player.FindAction("Menu", throwIfNotFound: true);
        m_Player_ConsumeCandy = m_Player.FindAction("ConsumeCandy", throwIfNotFound: true);
        m_Player_Block = m_Player.FindAction("Block", throwIfNotFound: true);
        m_Player_SwitchToolBefore = m_Player.FindAction("SwitchToolBefore", throwIfNotFound: true);
        m_Player_SwitchToolAfter = m_Player.FindAction("SwitchToolAfter", throwIfNotFound: true);
        // Player1
        m_Player1 = asset.FindActionMap("Player1", throwIfNotFound: true);
        m_Player1_Move = m_Player1.FindAction("Move", throwIfNotFound: true);
        m_Player1_Sprint = m_Player1.FindAction("Sprint", throwIfNotFound: true);
        m_Player1_Jump = m_Player1.FindAction("Jump", throwIfNotFound: true);
        m_Player1_Lock = m_Player1.FindAction("Lock", throwIfNotFound: true);
        m_Player1_Interact = m_Player1.FindAction("Interact", throwIfNotFound: true);
        m_Player1_AttackUse = m_Player1.FindAction("AttackUse", throwIfNotFound: true);
        m_Player1_Menu = m_Player1.FindAction("Menu", throwIfNotFound: true);
        m_Player1_Block = m_Player1.FindAction("Block", throwIfNotFound: true);
        m_Player1_ConsumeCandy = m_Player1.FindAction("ConsumeCandy", throwIfNotFound: true);
        m_Player1_SwitchToolBefore = m_Player1.FindAction("SwitchToolBefore", throwIfNotFound: true);
        m_Player1_SwitchToolAfter = m_Player1.FindAction("SwitchToolAfter", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Lock;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_AttackUse;
    private readonly InputAction m_Player_Menu;
    private readonly InputAction m_Player_ConsumeCandy;
    private readonly InputAction m_Player_Block;
    private readonly InputAction m_Player_SwitchToolBefore;
    private readonly InputAction m_Player_SwitchToolAfter;
    public struct PlayerActions
    {
        private @Movements m_Wrapper;
        public PlayerActions(@Movements wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Lock => m_Wrapper.m_Player_Lock;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @AttackUse => m_Wrapper.m_Player_AttackUse;
        public InputAction @Menu => m_Wrapper.m_Player_Menu;
        public InputAction @ConsumeCandy => m_Wrapper.m_Player_ConsumeCandy;
        public InputAction @Block => m_Wrapper.m_Player_Block;
        public InputAction @SwitchToolBefore => m_Wrapper.m_Player_SwitchToolBefore;
        public InputAction @SwitchToolAfter => m_Wrapper.m_Player_SwitchToolAfter;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Lock.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLock;
                @Lock.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLock;
                @Lock.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLock;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @AttackUse.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackUse;
                @AttackUse.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackUse;
                @AttackUse.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackUse;
                @Menu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                @ConsumeCandy.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnConsumeCandy;
                @ConsumeCandy.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnConsumeCandy;
                @ConsumeCandy.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnConsumeCandy;
                @Block.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                @SwitchToolBefore.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchToolBefore;
                @SwitchToolBefore.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchToolBefore;
                @SwitchToolBefore.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchToolBefore;
                @SwitchToolAfter.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchToolAfter;
                @SwitchToolAfter.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchToolAfter;
                @SwitchToolAfter.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchToolAfter;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Lock.started += instance.OnLock;
                @Lock.performed += instance.OnLock;
                @Lock.canceled += instance.OnLock;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @AttackUse.started += instance.OnAttackUse;
                @AttackUse.performed += instance.OnAttackUse;
                @AttackUse.canceled += instance.OnAttackUse;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @ConsumeCandy.started += instance.OnConsumeCandy;
                @ConsumeCandy.performed += instance.OnConsumeCandy;
                @ConsumeCandy.canceled += instance.OnConsumeCandy;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @SwitchToolBefore.started += instance.OnSwitchToolBefore;
                @SwitchToolBefore.performed += instance.OnSwitchToolBefore;
                @SwitchToolBefore.canceled += instance.OnSwitchToolBefore;
                @SwitchToolAfter.started += instance.OnSwitchToolAfter;
                @SwitchToolAfter.performed += instance.OnSwitchToolAfter;
                @SwitchToolAfter.canceled += instance.OnSwitchToolAfter;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Player1
    private readonly InputActionMap m_Player1;
    private IPlayer1Actions m_Player1ActionsCallbackInterface;
    private readonly InputAction m_Player1_Move;
    private readonly InputAction m_Player1_Sprint;
    private readonly InputAction m_Player1_Jump;
    private readonly InputAction m_Player1_Lock;
    private readonly InputAction m_Player1_Interact;
    private readonly InputAction m_Player1_AttackUse;
    private readonly InputAction m_Player1_Menu;
    private readonly InputAction m_Player1_Block;
    private readonly InputAction m_Player1_ConsumeCandy;
    private readonly InputAction m_Player1_SwitchToolBefore;
    private readonly InputAction m_Player1_SwitchToolAfter;
    public struct Player1Actions
    {
        private @Movements m_Wrapper;
        public Player1Actions(@Movements wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player1_Move;
        public InputAction @Sprint => m_Wrapper.m_Player1_Sprint;
        public InputAction @Jump => m_Wrapper.m_Player1_Jump;
        public InputAction @Lock => m_Wrapper.m_Player1_Lock;
        public InputAction @Interact => m_Wrapper.m_Player1_Interact;
        public InputAction @AttackUse => m_Wrapper.m_Player1_AttackUse;
        public InputAction @Menu => m_Wrapper.m_Player1_Menu;
        public InputAction @Block => m_Wrapper.m_Player1_Block;
        public InputAction @ConsumeCandy => m_Wrapper.m_Player1_ConsumeCandy;
        public InputAction @SwitchToolBefore => m_Wrapper.m_Player1_SwitchToolBefore;
        public InputAction @SwitchToolAfter => m_Wrapper.m_Player1_SwitchToolAfter;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMove;
                @Sprint.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSprint;
                @Jump.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnJump;
                @Lock.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnLock;
                @Lock.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnLock;
                @Lock.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnLock;
                @Interact.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnInteract;
                @AttackUse.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAttackUse;
                @AttackUse.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAttackUse;
                @AttackUse.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAttackUse;
                @Menu.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMenu;
                @Block.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnBlock;
                @ConsumeCandy.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnConsumeCandy;
                @ConsumeCandy.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnConsumeCandy;
                @ConsumeCandy.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnConsumeCandy;
                @SwitchToolBefore.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSwitchToolBefore;
                @SwitchToolBefore.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSwitchToolBefore;
                @SwitchToolBefore.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSwitchToolBefore;
                @SwitchToolAfter.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSwitchToolAfter;
                @SwitchToolAfter.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSwitchToolAfter;
                @SwitchToolAfter.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSwitchToolAfter;
            }
            m_Wrapper.m_Player1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Lock.started += instance.OnLock;
                @Lock.performed += instance.OnLock;
                @Lock.canceled += instance.OnLock;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @AttackUse.started += instance.OnAttackUse;
                @AttackUse.performed += instance.OnAttackUse;
                @AttackUse.canceled += instance.OnAttackUse;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @ConsumeCandy.started += instance.OnConsumeCandy;
                @ConsumeCandy.performed += instance.OnConsumeCandy;
                @ConsumeCandy.canceled += instance.OnConsumeCandy;
                @SwitchToolBefore.started += instance.OnSwitchToolBefore;
                @SwitchToolBefore.performed += instance.OnSwitchToolBefore;
                @SwitchToolBefore.canceled += instance.OnSwitchToolBefore;
                @SwitchToolAfter.started += instance.OnSwitchToolAfter;
                @SwitchToolAfter.performed += instance.OnSwitchToolAfter;
                @SwitchToolAfter.canceled += instance.OnSwitchToolAfter;
            }
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLock(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAttackUse(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnConsumeCandy(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnSwitchToolBefore(InputAction.CallbackContext context);
        void OnSwitchToolAfter(InputAction.CallbackContext context);
    }
    public interface IPlayer1Actions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLock(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAttackUse(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnConsumeCandy(InputAction.CallbackContext context);
        void OnSwitchToolBefore(InputAction.CallbackContext context);
        void OnSwitchToolAfter(InputAction.CallbackContext context);
    }
}
