# DevOps CI/CD for Services

One of the primary benefits of DevOps is to decrease the latency between development changes (features, bug fixes, etc.) and the business or end user seeing the affect of those changes in Operation.

In order to decrease this latency, we create pipelines that move changes from the source to deployment in an automated, dependable, and reliable fashion.  Our reference application uses a combination of VSTS CI/CD services to deploy the back-end services and the Mobile Application Center to handle the Client.

The following steps are in place for the Service deployment:
<OL>
<li>Restore Dependencies</li>
<li>Build Agent</li>
<li>Build Tests</li>
<li>Execute Tests</li>
<li>Create Publishable Artifacts</li>
<li>Deploy service to Production</li>
</OL>

The following screenshot shows the VSTS build Definition:
![Build Definition](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_DevOps_01.PNG)

## Unit Testing
One of the best mechanism to ensure that functionality is still reliable is to implement testing prior to moving code into production.  As a part of the pipeline, unit tests can run preventing unsatisfactory behavior from being deployed, as shown below in this build report:

![DevOps Tesing Pipeline](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_DevOps_02.PNG)


## Automated Deployment

Finally, the service can be automatically deployed into operations.  This may be a development, testing, staging, or even production environments.

<UL>
References:
<li><a href="https://blogs.msdn.microsoft.com/visualstudioalmrangers/2017/10/04/azure-function-ci-cd-devops-pipeline/">Azure Functions – CI / CD DevOps Pipeline</a></li>
<li><a href="https://blogs.msdn.microsoft.com/visualstudioalmrangers/2017/09/12/azure-function-provisioning-and-configuring-our-azure-function-infrastructure/">Azure Function – Provisioning and configuring our Azure Function infrastructure</a></li>
</UL>