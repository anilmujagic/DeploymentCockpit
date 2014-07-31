﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Services
{
    public class DeploymentPlanStepService : CrudService<DeploymentPlanStep>, IDeploymentPlanStepService
    {
        public DeploymentPlanStepService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public DeploymentPlanStep GetWithRelations(int id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<DeploymentPlanStep>()
                    .GetAll(
                        s => s.DeploymentPlanStepID == id,
                        s => s.Script,
                        s => s.TargetGroup)
                    .SingleOrDefault();
            }
        }

        public IEnumerable<DeploymentPlanStep> GetAllForDeploymentPlan(short deploymentPlanID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<DeploymentPlanStep>()
                    .GetAll(s => s.DeploymentPlanID == deploymentPlanID);
            }
        }

        public IEnumerable<TDto> GetAllForDeploymentPlanAs<TDto>(short deploymentPlanID)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<DeploymentPlanStep>()
                    .GetAllAs<TDto, Script, TargetGroup>(
                        s => s.DeploymentPlanID == deploymentPlanID,
                        s => s.Script,
                        s => s.TargetGroup);
            }
        }
    }
}