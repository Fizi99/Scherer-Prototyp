using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehaviourManager : MonoBehaviour
{

    /*
    public List<Task> taskList = new List<Task>();

    string[] possibleTasks = { "wander", "wait", "flee", "alert", "follow", "agitated" };

    private Task defaultTask = new Task("wait", 0, "neutral_idle");

    private NpcAnimationManager animManager;

    private void Start(){
        taskList.Add(defaultTask);
        animManager = transform.GetComponent<NpcAnimationManager>();

    }

    private void Update(){
        HandleTasks();
    }

    /// <summary>
    /// Add a new task. depending on its prio it gets added in the Task stack.
    /// </summary>
    /// <param name="task">Task that gets added.</param>
    public void AddTask(Task task){

        bool inserted = false;

        for(int i = 0; i < taskList.Count; i++){

            if(task.prio > taskList[i].prio){

                taskList.Insert(i, task);
                inserted = true;
                break;
            }
        }

        //if it has lowest prio add task last
        if(!inserted){
            taskList.Add(task);
        }
    }

    public void EmptyTasks(){
        taskList.Clear();
        taskList.Add(defaultTask);
    }

    public void CancelTask(){

        if(taskList[0].rigid != null){
            taskList[0].rigid.velocity = new Vector3(0, 0, 0);
        }
        taskList[0].taskConcluded = true;
    }
    private void HandleTasks(){

        //if task has concluded remove it and check if task stack is empty. In case it is, add defaulttask.
        if(taskList[0].taskConcluded){
            taskList.RemoveAt(0);
            if(taskList.Count <= 0){
                taskList.Add(defaultTask);
            }
        }
        if (taskList[0].name == "wander") {
            Wander(taskList[0]);
        } else if (taskList[0].name == "wait") {
            Wait(taskList[0]);
        } else if (taskList[0].name == "flee") {
            Flee(taskList[0]);
        } else if (taskList[0].name == "follow"){
            Follow(taskList[0]);
        }

    }

    /// <summary>
    /// Complete the Wandering task. Npc moves to a random point.
    /// </summary>
    /// <param name="task">The currently worked on task</param>
    private void Wander(Task task){

        if(!task.taskStarted){
            task.taskStarted = true;
            //task.prio = 0;
            animManager.ChangeAnimation(task.animation);

            //task specific initialization
            task.destination = PickRandTarget(new Vector3(-20,0,-20), new Vector3(20,0,20));
            task.speed = Random.Range(0.5f, 2f);
            task.rigid = transform.GetComponent<Rigidbody>();
        }
        if(!((transform.position - task.destination).magnitude <= 0.1)){
            Move(task.rigid, task.destination, task.speed);
        }else{
            task.rigid.velocity = new Vector3(0, 0, 0);
            task.taskConcluded = true;
        }
    }

    /// <summary>
    /// Complete the wait task. The Npc waits for a random amount of time.
    /// </summary>
    /// <param name="task">The currently worked on task</param>
    private void Wait(Task task){

        if (!task.taskStarted){
            task.taskStarted = true;
            //task.prio = 0;
            animManager.ChangeAnimation(task.animation);

            //task specific initialization
            task.waitTime = 5f;//Random.Range(1f, 5f);
            task.passedWaitTime = 0;
        }
        if(task.passedWaitTime < task.waitTime){
            task.passedWaitTime += Time.deltaTime;
        }else{
            task.taskConcluded = true;
        }
    }

    /// <summary>
    /// Complete the flee task. The Npc flees from player until defined distance is reached.
    /// </summary>
    /// <param name="task">The currently worked on task</param>
    private void Flee(Task task){

        if(!task.taskStarted){
            task.taskStarted = true;
            //task.prio = 0;
            animManager.ChangeAnimation(task.animation);

            //task specific initialization
            task.speed = 3;
            task.rigid = transform.GetComponent<Rigidbody>();
            task.target = GameObject.Find("Player").transform;
        }
        if((transform.position - task.target.position).magnitude <= task.range){

            task.destination = task.target.transform.position + ((transform.position - new Vector3(task.target.transform.position.x, 0, task.target.transform.position.z)).normalized * task.range);
            Move(task.rigid, task.destination, task.speed);
        }else{
            task.rigid.velocity = new Vector3(0, 0, 0);
            task.taskConcluded = true;
        }
    }

    /// <summary>
    /// Complete the follow task. The Npc follows the player until certain distance to player is reached.
    /// </summary>
    /// <param name="task">The currently worked on task</param>
    private void Follow(Task task){

        if (!task.taskStarted){
            task.taskStarted = true;
            //task.prio = 0;
            animManager.ChangeAnimation(task.animation);

            //task specific initialization
            task.speed = 3;
            task.rigid = transform.GetComponent<Rigidbody>();
            task.target = GameObject.Find("Player").transform;
        }
        if (!((transform.position - task.target.position).magnitude <= task.range)){
            task.destination = task.target.position;
            Move(task.rigid, task.destination, task.speed);
        }else{
            task.rigid.velocity = new Vector3(0, 0, 0);
            task.taskConcluded = true;
        }
    }

    /// <summary>
    /// returns a random Vector in bounds
    /// </summary>
    /// <param name="min">included min bounds </param>
    /// <param name="max">excluded max bounds</param>
    /// <returns>returns selected vector</returns>
    private Vector3 PickRandTarget(Vector3 min, Vector3 max){
        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));

    }

    /// <summary>
    /// Only moves rigidbody towards destination with certain speed
    /// </summary>
    /// <param name="rigid">the rigidbody, that is moved</param>
    /// <param name="target">the destination</param>
    /// <param name="speed">the travelspeed</param>
    private void Move(Rigidbody rigid, Vector3 destination, float speed){
        Debug.Log(rigid.velocity);
        rigid.velocity = (destination - rigid.transform.position).normalized * speed;
        transform.LookAt(destination);
    }
    */



    public Task currentTask = new Task("wait", 1f, "neutral_idle");
    private NpcAnimationManager animManager;
    private Transform player;

    private Rigidbody rigid;
    private Vector3 targetLocation;
    private Vector3 direction;

    private List<Task> taskList = new List<Task>();
    private int taskIndex = 0;

    private void Start()
    {
        animManager = transform.GetComponent<NpcAnimationManager>();
        player = GameObject.Find("Player").transform;

        rigid = transform.GetComponent<Rigidbody>();

        AddToTaskList(new Task("wait", 1f, "neutral_idle"));
        AddToTaskList(new Task("wander", 3f, "neutral_walk"));

        currentTask = taskList[0];

    }

    private void Update()
    {

        if (currentTask.PassTime())
        {
            HandleTasks();
        }
        else
        {
            NextTask();
        }
        
    }

    public void StartTaskList()
    {
        taskIndex = 0;
        currentTask = taskList[0];
        animManager.ChangeAnimation(taskList[0].animation);
    }

    public void StartTask(Task task)
    {
        currentTask = task;
        animManager.ChangeAnimation(task.animation);

    }

    public void NextTask()
    {
        currentTask.Reset();
        taskIndex++;
        if (taskIndex >= taskList.Count)
        {
            taskIndex = 0;
        }

        StartTask(taskList[taskIndex]);
    }

    private void HandleTasks()
    {
        if (currentTask.name == "wait")
        {
            Wait();
        }else if (currentTask.name == "wander")
        {
            Wander();
        }else if (currentTask.name == "flee")
        {
            Flee();
        }
        else if (currentTask.name == "waitFlee")
        {
            WaitFlee();
        }
        else if (currentTask.name == "follow")
        {
            Follow();
        }
        else if (currentTask.name == "waitFollow")
        {
            WaitFollow();
        }
        else if (currentTask.name == "followNpc")
        {
            FollowNpc();
        }
        else if (currentTask.name == "waitFollowNpc")
        {
            WaitFollowNpc();
        }
        else if (currentTask.name == "fleeNpc")
        {
            FleeNpc();
        }
        else if (currentTask.name == "waitFleeNpc")
        {
            WaitFleeNpc();
        }
    }

    public void AddToTaskList(Task task)
    {
        taskList.Add(task);
    }

    public void ResetTaskList()
    {
        taskList.Clear();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////TASKHANDLING////////////////////////////////////////////////////
    private void Wait()
    {
        if (!currentTask.taskStarted)
        {
            currentTask.taskStarted = true;
            currentTask.passedTime = 0;
        }
        Move(0);
    }

    private void Wander()
    {
        if (!currentTask.taskStarted)
        {
            currentTask.taskStarted = true;
            currentTask.passedTime = 0;
            targetLocation = PickRandTarget(new Vector3(-20, 0, -20), new Vector3(20, 0, 20));
        }

        if((targetLocation - rigid.transform.position).magnitude < 0.1f)
        {
            NextTask();
        }
        else
        {
            Move(3);
        }
        
    }

    private void Flee()
    {
        if (!currentTask.taskStarted)
        {
            currentTask.taskStarted = true;
            currentTask.passedTime = 0;
        }

        targetLocation = new Vector3(player.position.x, 0, player.position.z) + ((new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(player.position.x, 0, player.position.z)).normalized * 10f);

        if ((targetLocation - rigid.transform.position).magnitude < 0.1f || (player.position - rigid.transform.position).magnitude > 9f)
        {
            NextTask();
        }
        else
        {
            Move(5);
        }
    }

    private void WaitFlee()
    {
        if (!currentTask.taskStarted)
        {
            currentTask.taskStarted = true;
            currentTask.passedTime = 0;
        }

        targetLocation = new Vector3(player.position.x, 0, player.position.z);

        if ((player.position - rigid.transform.position).magnitude < 9f)
        {
            NextTask();
        }
        else
        {
            Move(0);
        }
    }

    private void Follow()
    {
        if (!currentTask.taskStarted)
        {
            currentTask.taskStarted = true;
            currentTask.passedTime = 0;
        }

        targetLocation = new Vector3(player.position.x, 0, player.position.z);

        if ((targetLocation - rigid.transform.position).magnitude < 2f)
        {
            NextTask();
        }
        else
        {
            Move(5);
        }
    }

    private void WaitFollow()
    {
        if (!currentTask.taskStarted)
        {
            currentTask.taskStarted = true;
            currentTask.passedTime = 0;
        }

        targetLocation = new Vector3(player.position.x, 0, player.position.z);

        if ((player.position - rigid.transform.position).magnitude > 2.5f)
        {
            NextTask();
        }
        else
        {
            Move(0);
        }
    }

    private void FollowNpc()
    {
        if (!currentTask.taskStarted)
        {
            currentTask.taskStarted = true;
            currentTask.passedTime = 0;
        }

        targetLocation = new Vector3(currentTask.targetNpc.position.x, 0, currentTask.targetNpc.position.z);

        if ((targetLocation - rigid.transform.position).magnitude < 4.5f)
        {
            NextTask();
        }
        else
        {
            Move(5);
        }
    }

    private void WaitFollowNpc()
    {
        if (!currentTask.taskStarted)
        {
            currentTask.taskStarted = true;
            currentTask.passedTime = 0;
        }

        targetLocation = new Vector3(currentTask.targetNpc.position.x, 0, currentTask.targetNpc.position.z);

        if ((targetLocation - rigid.transform.position).magnitude > 5f)
        {
            NextTask();
        }
        else
        {
            Move(0);
        }
    }

    private void FleeNpc()
    {
        if (!currentTask.taskStarted)
        {
            currentTask.taskStarted = true;
            currentTask.passedTime = 0;
        }
        targetLocation = new Vector3(currentTask.targetNpc.position.x, 0, currentTask.targetNpc.position.z) + ((new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(currentTask.targetNpc.position.x, 0, currentTask.targetNpc.position.z).normalized) * 10f);

        if ((targetLocation - rigid.transform.position).magnitude < 0.1f || (new Vector3(currentTask.targetNpc.position.x, 0, currentTask.targetNpc.position.z) - rigid.transform.position).magnitude > 9f)
        {
            NextTask();
        }
        else
        {
            Move(5);
        }
    }

    private void WaitFleeNpc()
    {
        if (!currentTask.taskStarted)
        {
            currentTask.taskStarted = true;
            currentTask.passedTime = 0;
        }

        targetLocation = new Vector3(currentTask.targetNpc.position.x, 0, currentTask.targetNpc.position.z);

        if ((new Vector3(currentTask.targetNpc.position.x, 0, currentTask.targetNpc.position.z) - rigid.transform.position).magnitude < 9f)
        {
            NextTask();
        }
        else
        {
            Move(0);
        }
    }





    private void Move(float speed)
    {
        direction = (targetLocation - rigid.transform.position).normalized;
        rigid.velocity = direction * speed;
        transform.LookAt(targetLocation);
    }

    private Vector3 PickRandTarget(Vector3 min, Vector3 max)
    {
        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));

    }

}
