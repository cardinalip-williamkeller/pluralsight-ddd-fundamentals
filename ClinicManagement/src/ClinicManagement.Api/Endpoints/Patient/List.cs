﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorShared.Models.Patient;
using ClinicManagement.Domain.Specifications.Client;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using PluralsightDdd.SharedKernel.Interfaces;
using IMapper = AutoMapper.IMapper;

namespace ClinicManagement.Api.Endpoints.Patient
{
  public class List : Endpoint<ListPatientRequest, Results<Ok<ListPatientResponse>, NotFound>>
  {
    private readonly IRepository<Domain.Aggregates.ClientAggregate.Client> _repository;
    private readonly IMapper _mapper;

    public List(IRepository<Domain.Aggregates.ClientAggregate.Client> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public override void Configure()
    {
      Get("api/patients");
      AllowAnonymous();
      Description(d =>
          d.WithSummary("List Patients")
           .WithDescription("List Patients")
           .WithName("patients.List")
           .WithTags("PatientEndpoints"));
    }

    public override async Task<Results<Ok<ListPatientResponse>, NotFound>> ExecuteAsync(ListPatientRequest request, CancellationToken cancellationToken)
    {
      var response = new ListPatientResponse(request.CorrelationId);

      var spec = new ClientByIdIncludePatientsSpec(request.ClientId);
      var client = await _repository.GetBySpecAsync(spec);
      if (client == null) return TypedResults.NotFound();

      response.Patients = _mapper.Map<List<PatientDto>>(client.Patients);
      response.Count = response.Patients.Count;

      return TypedResults.Ok(response);
    }
  }
}
