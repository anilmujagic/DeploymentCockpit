using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DeploymentCockpit.ApiDtos;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Common
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Credential, CredentialDto>();
            Mapper.CreateMap<CredentialDto, Credential>();

            Mapper.CreateMap<DeploymentJob, DeploymentJobDto>();
            Mapper.CreateMap<DeploymentJobDto, DeploymentJob>();
            
            Mapper.CreateMap<DeploymentJobStep, DeploymentJobStepDto>();
            Mapper.CreateMap<DeploymentJobStepDto, DeploymentJobStep>();
            
            Mapper.CreateMap<DeploymentJobStepTarget, DeploymentJobStepTargetDto>();
            Mapper.CreateMap<DeploymentJobStepTargetDto, DeploymentJobStepTarget>();
            
            Mapper.CreateMap<DeploymentJobStepDto, DeploymentJobStepFlatDto>();
            Mapper.CreateMap<DeploymentJobStepTargetDto, DeploymentJobStepFlatDto>();
            Mapper.CreateMap<DeploymentJobStep, ExecutionDetails>();
            Mapper.CreateMap<DeploymentJobStepTarget, ExecutionDetails>();

            Mapper.CreateMap<DeploymentPlan, DeploymentPlanDto>();
            Mapper.CreateMap<DeploymentPlanDto, DeploymentPlan>();
            
            Mapper.CreateMap<DeploymentPlanStep, DeploymentPlanStepDto>();
            Mapper.CreateMap<DeploymentPlanStepDto, DeploymentPlanStep>();
            
            Mapper.CreateMap<Project, ProjectDto>();
            Mapper.CreateMap<ProjectDto, Project>();
            
            Mapper.CreateMap<ProjectEnvironment, ProjectEnvironmentDto>();
            Mapper.CreateMap<ProjectEnvironmentDto, ProjectEnvironment>();
            
            Mapper.CreateMap<ProjectTarget, ProjectTargetDto>();
            Mapper.CreateMap<ProjectTargetDto, ProjectTarget>();
            
            Mapper.CreateMap<Script, ScriptDtoForList>();
            Mapper.CreateMap<Script, ScriptDto>();
            Mapper.CreateMap<ScriptDto, Script>();
            
            Mapper.CreateMap<ScriptParameter, ScriptParameterDto>();
            Mapper.CreateMap<ScriptParameterDto, ScriptParameter>();
            
            Mapper.CreateMap<Target, TargetDto>();
            Mapper.CreateMap<TargetDto, Target>();
            
            Mapper.CreateMap<TargetGroup, TargetGroupDto>();
            Mapper.CreateMap<TargetGroupDto, TargetGroup>();
            
            Mapper.CreateMap<Variable, VariableDto>();
            Mapper.CreateMap<VariableDto, Variable>();
        }
    }
}
