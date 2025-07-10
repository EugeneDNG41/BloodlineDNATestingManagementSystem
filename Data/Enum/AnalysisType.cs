using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enum
{
    public enum AnalysisType
    {
        // Bloodline & Relationship
        Paternity,
        Maternity,
        Sibling,
        TwinZygosity,
        Grandparentage,
        Avuncular,         // Uncle/Aunt relationship
        YChromosome,       // Paternal lineage
        MitochondrialDNA,  // Maternal lineage
        GeneticProfile,    // Generic DNA fingerprinting

        // Ancestry
        EthnicityEstimate,
        AncestryComposition,
        PopulationMatch,
        HaplogroupDetermination,

        // Legal/Immigration
        ImmigrationDNA,
        CourtAdmissiblePaternity,

        // Health-related (if service expands)
        CarrierStatus,
        GeneticHealthRisk,
        Pharmacogenetics,

        // Forensics or other
        ForensicMatch,
        UnknownSampleIdentification,
        DNAStorageOnly
    }
}
